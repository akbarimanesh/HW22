


using System.ComponentModel.DataAnnotations;
namespace App.Domain.Core.Entities
{
    public class TaskU
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "انتخاب عنوان وظیفه اجباری است")]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "لطفاً وضعیت وظیفه را مشخص کنید.")]
        public bool IsCompleted { get; set; } = false;
        public int UserTaId { get; set; }
         public UserTa? UserTa { get; set; }
       
    }
}
