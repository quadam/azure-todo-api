using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TodoApi.Entities;
using TodoApi.Models;

namespace TodoApi.AutoMapping
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<TodoEntity, TodoModel>()
                .ForMember(model => model.Group, opts => opts.MapFrom(entity => entity.PartitionKey))
                .ForMember(model => model.Id, opts => opts.MapFrom(entity => entity.RowKey))
                .ReverseMap();
        }
    }
}
