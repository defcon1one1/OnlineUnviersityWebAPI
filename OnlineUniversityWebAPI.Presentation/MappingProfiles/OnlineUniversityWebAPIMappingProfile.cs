using AutoMapper;
using OnlineUniversityWebAPI.Application.Models.Dtos;
using OnlineUniversityWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUniversityWebAPI.Presentation.MappingProfiles
{
    public class OnlineUniversityWebAPIMappingProfile : Profile
    {
        public OnlineUniversityWebAPIMappingProfile()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();


            CreateMap<EnrollmentDto, Enrollment>();
            CreateMap<Enrollment, EnrollmentDto>();
            CreateMap<CreateEnrollmentDto, Enrollment>();

            CreateMap<Grade, GradeDto>();
            CreateMap<GradeDto, Grade>();

            CreateMap<Course, CourseDto>();
            CreateMap<CourseDto, Course>();

            CreateMap<ModuleDto, Module>();
            CreateMap<Module, ModuleDto>();
        }
    }
}
