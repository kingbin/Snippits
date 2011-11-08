//Controller Code:

List<SelectListItem> list = ( from x in MyEntities.vw_SomeData
                              select ( new SelectListItem() { Text = x.TextToView, Value = x.DataValueID } ) ).ToList<SelectListItem>();

ViewBag.MyDDL = list;


//RAZOR
@(Html.DropDownListFor( model => model.myValID, (List<SelectListItem>)ViewBag.DataValueID , "Select One"))





// I like the first solution, but I've been using the following in my applications since when razor was introduced.
// Given the fact there was hardly any publications on razor when I started using it, I used the following in my code.
/***********************************************************************************************************/
//RAZOR Template Code
  List<SelectListItem> departments = new List<SelectListItem>();

  var departmentQuery = resourcePoolEntities.vw_NurseDirectorEmployeeMappedDepartments.Where( d => d.Employee == Model.nurseRequest.Employee ).Select( a => a );

// if in edit mode include: Selected = Model.Department == d.Department to match previously entered data
  foreach( vw_NurseDirectorEmployeeMappedDepartments s in departmentQuery ) {
    departments.Add( new SelectListItem() { Text = s.DepartmentDescription, Value = s.Department } );
  };



//RAZOR

@(Html.DropDownListFor( model => model.nurseRequest.Department, departments, "Please Select a Department" ))


