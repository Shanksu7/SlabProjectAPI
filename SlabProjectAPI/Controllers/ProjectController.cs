﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SlabProject.Entity.Constants;
using SlabProject.Entity.Requests;
using SlabProject.Entity.Responses;
using SlabProjectAPI.Services.Interfaces;
using System.Threading.Tasks;

namespace SlabProjectAPI.Controllers
{
    [ApiController]
    [Route("project")]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IProjectService _projectService;

        public ProjectController(
            ILogger<ProjectController> logger,
            IProjectService projectService
            )
        {
            _logger = logger;
            _projectService = projectService;
        }

        /// <summary>
        /// Create a project based on the model
        /// </summary>
        /// <param name="model">Project creation model</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Operator)]
        public IActionResult CreateProject([FromBody] CreateProjectRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = _projectService.CreateProject(model);
                if (result.Success)
                    return CreatedAtAction(nameof(GetProjectById), new { id = result.Data.Id }, result);
                else
                    return BadRequest(result);
            }
            else
            {
                return BadRequest(new BaseRequestResponse<bool>()
                {
                    Errors = new System.Collections.Generic.List<string>()
                    {
                        "Invalid payload"
                    }
                });
            }
        }

        /// <summary>
        /// Create a Task for a project using the project ID
        /// </summary>
        /// <param name="model">Project Task creation model</param>
        /// <returns></returns>
        [HttpPost]
        [Route("task")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Operator)]
        public IActionResult CreateProjectTask([FromBody] CreateTaskRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = _projectService.CreateTask(model);
                if (result.Success)
                    return CreatedAtAction(nameof(GetTaskById), new { id = result.Data.Id }, result);
                else
                    return BadRequest(result);
            }
            else
            {
                return BadRequest(new BaseRequestResponse<bool>()
                {
                    Errors = new System.Collections.Generic.List<string>()
                    {
                        "Invalid payload"
                    }
                });
            }
        }

        /// <summary>
        /// Get a project based on its Id
        /// </summary>
        /// <param name="id">Project's id to find</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Admin)]
        public IActionResult GetProjectById(int id)
        {
            var result = _projectService.GetProject(id);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        /// <summary>
        /// Get a task based on its Id
        /// </summary>
        /// <param name="id">Task's id to find</param>
        /// <returns></returns>
        [HttpGet("task/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Admin)]
        public IActionResult GetTaskById(int id)
        {
            var result = _projectService.GetTask(id);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        /// <summary>
        /// Get all projects information
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Admin)]
        public IActionResult GetProjects()
        {
            var result = _projectService.GetProjects();
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        /// <summary>
        /// Get all Tasks information
        /// </summary>
        /// <returns></returns>
        [HttpGet("allTasks")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Admin)]
        public IActionResult GetTasks()
        {
            var result = _projectService.GetTasks();
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        /// <summary>
        /// Update a project based on the edit model
        /// </summary>
        /// <param name="model">Model for edit a project, all properties are optional, except Project's Id</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Operator)]
        public IActionResult UpdateProject(int id, [FromBody] EditProjectRequest model)
        {
            var result = _projectService.EditProject(id, model);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        /// <summary>
        /// Update a task based on the edit model
        /// </summary>
        /// <param name="model">Model for edit a task, all properties are optional, except task's Id</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("task/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Operator)]
        public IActionResult UpdateTask(int id, [FromBody] EditTaskRequest model)
        {
            var result = _projectService.EditTask(id, model);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        /// <summary>
        /// Complete a project if all of its Tasks are already executed
        /// </summary>
        /// <param name="id">Project's Id</param>
        /// <returns></returns>
        [HttpPatch("complete/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Operator)]
        public async Task<IActionResult> CompleteProject(int id)
        {
            var result = await _projectService.CompleteProject(id);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        /// <summary>
        /// Complete a task
        /// </summary>
        /// <param name="id">Project's Id</param>
        /// <returns></returns>
        [HttpPatch("complete/task/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Operator)]
        public IActionResult CompleteProjectTask(int id)
        {
            var result = _projectService.CompleteTask(id);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        /// <summary>
        /// Delete a project based on its Id
        /// </summary>
        /// <param name="id">Project's Id to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Admin)]
        public IActionResult DeleteProject(int id)
        {
            var result = _projectService.DeleteProject(id);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        /// <summary>
        /// Delete a task based on its Id
        /// </summary>
        /// <param name="id">Task's Id to delete</param>
        /// <returns></returns>
        [HttpDelete("task/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Admin)]
        public IActionResult DeleteProjectTask(int id)
        {
            var result = _projectService.DeleteTask(id);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}