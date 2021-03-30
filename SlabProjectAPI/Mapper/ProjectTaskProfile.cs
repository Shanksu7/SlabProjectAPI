﻿using AutoMapper;
using SlabProjectAPI.Domain.Requests;
using SlabProjectAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlabProjectAPI.Mapper
{
    public class ProjectTaskProfile : Profile
    {
        public ProjectTaskProfile()
        {
            CreateMap<ProjectTask, ProjectTaskInfo>();
            CreateMap<ProjectTaskInfo, ProjectTask>();
            CreateMap<CreateTaskRequest, ProjectTask>();
        }
    }
}