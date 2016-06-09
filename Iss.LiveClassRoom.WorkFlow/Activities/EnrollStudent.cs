﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.ServiceLayer.Services;
using Iss.LiveClassRoom.DataAccessLayer;
using Iss.LiveClassRoom.Core.Models;
using System.ComponentModel;
using System.Threading.Tasks;
using Iss.LiveClassRoom.Core.Repositories;

namespace Iss.LiveClassRoom.WorkFlow.Activities
{

    public sealed class EnrollStudent : CodeActivity
    {
        public InArgument<string> CourseId { get; set; }

        public InArgument<string> StudentId { get; set; }

        public OutArgument<int> CurrentStudentNumber { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string courseId = context.GetValue(CourseId);
            string studentId = context.GetValue(StudentId);
            IUnitOfWork uow = new UnitOfWork(new SystemContext());
            ICourseService service = new CourseService(uow);
            IStudentService studentService = new StudentService(uow);
            Course course = service.GetById(courseId).Result;
            Student student = studentService.GetById(studentId).Result;
            System.Diagnostics.Debug.WriteLine("***" + course.Name);
            System.Diagnostics.Debug.WriteLine("***" + student.Name);
            service.AssignStudent(student, course, course.Instructor.Id).Wait();
            service.Update(course, course.Instructor.Id).Wait();
            System.Diagnostics.Debug.WriteLine("***Finish EnrollStudent Activity***");
        }
    }
}
