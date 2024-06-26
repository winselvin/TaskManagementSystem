﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class TaskManagementController : Controller
    {
        private readonly TaskManagementDBContext _context;
        public TaskManagementController(TaskManagementDBContext taskManagementDBContext)
        {
            _context = taskManagementDBContext;
        }

        /// <summary>
        /// To display all the task details
        /// </summary>
        /// <returns></returns>
        public IActionResult Dashboard(int departmentId)
        {
            var tasks = _context.Tasks.Include(st=>st.Status)
                                      .Include(cm=>cm.Comments).ToList();
            return View(tasks);
        }

        public ActionResult Add()
        {
            var users = _context.Users;
            ViewData["Users"] = new SelectList(users, "Id", "UserName");

            var statusOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "New" },
                new SelectListItem { Value = "2", Text = "In Progress" },
                new SelectListItem { Value = "3", Text = "Completed" }
            };
            ViewData["StatusOptions"] = statusOptions;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(TaskModel taskModel)
        {
            //if (ModelState.IsValid)
            //{
                var task = new Entity.Task
                {
                    Title = taskModel.Title,
                    Deadline = taskModel.Deadline,
                    StatusId = taskModel.StatusId,
                };

                foreach (var userId in taskModel.AssignedUserIds)
                {
                    var user = _context.Users.FirstOrDefault(user => user.Id == userId);
                    if(user != null)
                    {
                        task.TeamMembers.Add(user);
                    }
                }

                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard");
            //}

            return View(taskModel);
        }

        [HttpPost]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var taskEntity = await _context.Tasks.Include(st => st.Status)
                            .Include(cm => cm.Comments).FirstOrDefaultAsync(m => m.Id == id);


            TaskManagementSystem.Models.TaskModel taskModel = new TaskManagementSystem.Models.TaskModel
            {
                Id = taskEntity.Id,
                Title = taskEntity.Title,
                Deadline = taskEntity.Deadline,
                Status = new List<StatusModel>
                {
                   new StatusModel{ Id=taskEntity.Id,Status=taskEntity.Status.Name}

                }

            };


            if (taskModel == null)
            {
                return NotFound();
            }

            return View(taskModel);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var taskEntity = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);


            TaskManagementSystem.Models.TaskModel taskModel = new TaskManagementSystem.Models.TaskModel
            {
                Id = taskEntity.Id,
                Title = taskEntity.Title,
                Deadline = taskEntity.Deadline,
                StatusId = taskEntity.StatusId,

            };
            var statusOptions = _context.Statuses.ToList();
            ViewData["StatusOptions"] = new SelectList(statusOptions, "Id", "Name", taskEntity.StatusId);


            if (taskModel == null)
            {
                return NotFound();
            }

            return View(taskModel);
        }



        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}
