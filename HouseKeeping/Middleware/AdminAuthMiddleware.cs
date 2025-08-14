public class AdminAuthMiddleware
{
    private readonly RequestDelegate _next;

    public AdminAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLower();

        var protectedPaths = new[] {"/housekeepers", "/housekeeper" }; // all paths should be protected
        bool isProtected = protectedPaths.Any(path.StartsWith);
       
        // Check if it's an admin route
        if (isProtected)
        {
            var adminUserName = context.Session.GetString("AdminUserName");

            if (string.IsNullOrEmpty(adminUserName))
            {
                // Not logged in → redirect to login page
                context.Response.Redirect("/login");
                return;
            }
        }

        await _next(context);
    }
}
