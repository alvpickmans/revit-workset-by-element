#region Namespaces
using System;
using System.Linq;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace WorksetByElement
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public class UserWorksetSelectionFilter : ISelectionFilter
        {
            Document document { get; set; }
            public UserWorksetSelectionFilter(Document document)
            {
                this.document = document;
            }

            public bool AllowElement(Element el)
            {
                return IsUserWorksetElement(el.Id);
            }

            public bool AllowReference(Reference r, XYZ pt)
            {
                return false;
            }

            public bool IsUserWorksetElement(ElementId elId)
            {
                Element el = this.document.GetElement(elId);
                Workset ws = this.document.GetWorksetTable().GetWorkset(el.WorksetId);
                return ws.Kind == WorksetKind.UserWorkset;
            }
        }

        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Access current selection
            ICollection<ElementId> selected = uidoc.Selection.GetElementIds();
            UserWorksetSelectionFilter userWorksetFilter = new UserWorksetSelectionFilter(doc);

            try
            {
                ElementId selectionId;

                selectionId = selected.FirstOrDefault(id => userWorksetFilter.IsUserWorksetElement(id));

                while(selectionId == null)
                {
                    Reference elRef = uidoc.Selection.PickObject(ObjectType.Element, userWorksetFilter, "Select an element");
                    selectionId = doc.GetElement(elRef).Id;
                }

                Element element = doc.GetElement(selectionId);
                WorksetTable wsTable = doc.GetWorksetTable();
                Workset workset = wsTable.GetWorkset(element.WorksetId);
                wsTable.SetActiveWorksetId(element.WorksetId);

                TaskDialog.Show("Workset By Element", $"Active workset changed to '{workset.Name}'");
                return Result.Succeeded;
            }
            catch (Exception)
            {
                return Result.Failed;
            }
        }
    }
}
