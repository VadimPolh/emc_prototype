using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using MvcApp.Dal.Contexts;
using MvcApp.DalEntities.Entities;

namespace MvcApp.Dal.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MvcContext>
    {
        public Configuration()
        {
            // Add-Migration -StartUpProjectName MvcApp.Dal -ProjectName MvcApp.Dal -Name Initial
            // Update-Database -StartUpProjectName MvcApp.Dal -ProjectName MvcApp.Dal �TargetMigration: <NameOfChange>
            // Update-Database -StartUpProjectName MvcApp.Dal -ProjectName MvcApp.Dal
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MvcContext context)
        {
            #region Branches

            var branchList = new List<Branch>();
            foreach (var branch in new[] { "�������", "�������"})
            {
                branchList.Add(new Branch
                {
                    Id = branchList.Count + 1,
                    Name = branch,
                });
            }
            foreach (var element in branchList)
            {
                context.Set<Branch>().AddOrUpdate(x => x.Id, element);
            }

            #endregion

            #region Containers



            #endregion

            #region Courses

            var courseList = new List<Course>();
            foreach (var i in new[] { 1, 2, 3, 4, 5})
            {
                courseList.Add(new Course
                {
                    Id = i,
                    Number = i,
                });
            }
            foreach (var element in courseList)
            {
                context.Set<Course>().AddOrUpdate(x => x.Id, element);
            }

            #endregion

            #region Groups

            var groups = new List<Group>();
            foreach (var item in new[] { "�-", "�-", "�-", "�-", "�-", "�-" })
            {
                for (var i = 1; i < 5; i++)
                {
                    groups.Add(new Group
                    {
                        Id = groups.Count + 1,
                        GroupNumber = String.Format("{0}{1}", item, i + 100)
                    });
                }
            }
            foreach (var element in groups)
            {
                context.Set<Group>().AddOrUpdate(x => x.Id, element);
            }

            #endregion

            #region Lessons

            var lessons = new List<Lesson>();
            for (var i = 1; i < 30; i++)
            {
                lessons.Add(new Lesson
                {
                    Id = i,
                    Name = "������� " + i
                });
            }
            foreach (var element in lessons)
            {
                context.Set<Lesson>().AddOrUpdate(x => x.Id, element);
            }

            #endregion

            #region Lesson types

            var lessontypes = new List<LessonType>();
            foreach (var i in new[] { "������", "������������", "�����", "������������" })
            {
                lessontypes.Add(new LessonType
                {
                    Id = lessontypes.Count + 1,
                    Name = i
                });
            }
            foreach (var element in lessontypes)
            {
                context.Set<LessonType>().AddOrUpdate(x => x.Id, element);
            }

            #endregion

            #region Sets

            var sets = new List<Set>();
            foreach (var name in new[]{"�������������� ����������", "�������������", "������������"})
            {
                sets.Add(new Set
                             {
                                 Id = sets.Count + 1,
                                 Name = name
                             });
            }
            foreach (var element in sets)
            {
                context.Set<Set>().AddOrUpdate(x => x.Id, element);
            }

            #endregion

            #region Specialties

            var specList = new List<Specialty>();
            foreach (var name in new[] { "����", "��������� � ����������� ������������", "������������� ����", "������������","������������ ������������","���������� ����" })
            {
                specList.Add(new Specialty
                {
                    Id = specList.Count + 1,
                    Name = name,
                    Mentor = "���� ��������"
                });
            }
            foreach (var element in specList)
            {
                context.Set<Specialty>().AddOrUpdate(x => x.Id, element);
            }

            #endregion

            #region Students
            
            var studentNames = new[] { "����", "����", "������", "������", "����������", "�������", "�����", "�����", "�������", "���������" };
            var studentsFamilies = new[] { "������", "������", "�������", "�������", "�������", "�������" };
            var studentNamesGirls = new[] { "���������", "������", "����", "������", "���������" };
            var studentsFamiliesGirls = new[] { "�������", "���������", "��������", "�������", "��������", "��������" };

            var students = new List<Student>();
            foreach (var studentsFamily in studentsFamilies)
            {
                foreach (var studentName in studentNames)
                {
                    students.Add(new Student
                    {
                        Id = students.Count + 1,
                        Fio = studentsFamily + " " + studentName,
                        Age = students.Count % 5 + 20,
                    });
                }
            }
            foreach (var studentsFamily in studentsFamiliesGirls)
            {
                foreach (var studentName in studentNamesGirls)
                {
                    students.Add(new Student
                    {
                        Id = students.Count + 1,
                        Fio = studentsFamily + " " + studentName,
                        Age = students.Count % 5 + 20,
                    });
                }
            }
            foreach (var element in students)
            {
                context.Set<Student>().AddOrUpdate(x => x.Id, element);
            }

            #endregion

            #region Subjects

            var subjNames = new[] { "���������", "����������", "������", "����������������" };
            var subjects = new List<Subject>();
            foreach (var subjName in subjNames)
            {
                subjects.Add(new Subject
                {
                    Id = subjects.Count + 1,
                    Name = subjName
                });
            }
            foreach (var element in subjects)
            {
                context.Set<Subject>().AddOrUpdate(x => x.Id, element);
            }

            #endregion

            #region Teachers

            var teacherFamilies = new[] { "������", "�����������", "���������", "������", "��������" };
            var teacherNames = new[] { "���������", "�������", "����", "������", "�����", "����������" };
            var listTeachers = new List<Teacher>();
            foreach (var teacherFamily in teacherFamilies)
            {
                foreach (var teacherName in teacherNames)
                {
                    listTeachers.Add(new Teacher
                    {
                        Id = listTeachers.Count + 1,
                        Fio = teacherFamily + " " + teacherName,
                        Pass = (listTeachers.Count * listTeachers.Count).ToString(),
                        Exp = listTeachers.Count + 5
                    });
                }
            }
            foreach (var element in listTeachers)
            {
                context.Set<Teacher>().AddOrUpdate(x => x.Id, element);
            }

            #endregion

            


        }
    }
}
