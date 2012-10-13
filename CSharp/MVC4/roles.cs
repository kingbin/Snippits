
[HandleError]
 public class HomeController : Controller
 {
     [Authorize(Roles = "Admin, Super User")]
     public ActionResult AdministratorsOnly()
     {
         return View();
     }
 }


public class HomeController : Controller 
{
  public ActionResult AdministratorsOnly()
  {
    var roles = roleRepository.GetRolesForActivity("Administrators Only");
    authorizationService.RequireRoles(roles);

    return View();
  }
}



public class HomeController : Controller 
{
  public ActionResult AdministratorsOnly()
  {
    authorizationService.AuthorizeActivity("Administrators Only");

    return View();
  }
}


// http://lostechies.com/derickbailey/2011/05/24/dont-do-role-based-authorization-checks-do-activity-based-checks/#comment-2744
public class HomeController : Controller 
{
  [AuthorizeActivity] // run time reflection to know the activity and it neesd authorization 
  public ActionResult AdministratorsOnly()
  {
    authorizationService.AuthorizeActivity("Administrators Only");

    return View();
  }
}
