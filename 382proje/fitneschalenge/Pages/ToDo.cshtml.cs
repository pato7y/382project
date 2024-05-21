// using fitneschalenge.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using System.Collections.Generic;
// using System.Linq;

// namespace MyApp.Namespace // Ensure this matches your actual namespace
// {
//     public class ToDoModel : PageModel
//     {
//         private readonly UserToDoDatabaseContext _context;

//         public ToDoModel(UserToDoDatabaseContext context)
//         {
//             _context = context;
//         }

//         [BindProperty]
//         public TblTodo NewToDo { get; set; } = default!;

//         public List<TblTodo> ToDoList { get; set; } = default!;

//         public void OnGet()
//         {
//             // LINQ query to retrieve items where IsDeleted is false
//             ToDoList = (from item in _context.TblTodos
//                         where item.IsDeleted == false
//                         select item).ToList();
//         }

//         public IActionResult OnPost()
//         {
//             if (!ModelState.IsValid || NewToDo == null)
//             {
//                 return Page();
//             }

//             NewToDo.IsDeleted = false;
//             _context.TblTodos.Add(NewToDo);
//             _context.SaveChanges();
//             return RedirectToPage();
//         }

//         public IActionResult OnPostDelete(int id)
//         {
//             var todo = _context.TblTodos.Find(id);
//             if (todo != null)
//             {
//                 todo.IsDeleted = true;
//                 _context.SaveChanges();
//             }
//             return RedirectToPage();
//         }
//     }
// }


using fitneschalenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MyApp.Namespace
{
    public class ToDoModel : PageModel
    {
        private readonly UserToDoDatabaseContext _context;

        public ToDoModel(UserToDoDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TblTodo NewToDo { get; set; } = default!;

        public List<TblTodo> ToDoList { get; set; } = default!;

        public void OnGet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // LINQ query to retrieve items where IsDeleted is false and UserId is the logged-in user's id
            ToDoList = (from item in _context.TblTodos
                         where item.IsDeleted == false && item.UserId == userId
                         select item).ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewToDo == null)
            {
                return Page();
            }
            NewToDo.IsDeleted = false;
            NewToDo.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.TblTodos.Add(NewToDo);
            _context.SaveChanges();
            return RedirectToPage();
        }
    }
}
