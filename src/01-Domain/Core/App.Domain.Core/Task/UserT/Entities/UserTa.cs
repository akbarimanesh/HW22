using Microsoft.AspNetCore.Identity;



namespace App.Domain.Core.Entities
{
    public class UserTa : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<TaskU>? TaskUs { get; set; }
      
    }
}
