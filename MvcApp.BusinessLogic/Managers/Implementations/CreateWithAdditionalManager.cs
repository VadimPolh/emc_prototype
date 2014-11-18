using System.Collections.Generic;
using System.Linq;
using MvcApp.BusinessLogic.Managers.Base;
using MvcApp.BusinessLogic.Managers.Interfaces;
using MvcApp.BusinessLogic.Repositories.Interfaces;
using MvcApp.Dal.Interfaces;
using MvcApp.DalEntities.Entities;
using MvcApp.Models.Common;
using MvcApp.Models.Create;

namespace MvcApp.BusinessLogic.Managers.Implementations
{
    public class CreateWithAdditionalManager : BaseManager, ICreateWithAdditionalManager
    {
        public CreateWithAdditionalManager(IContext context, IBaseRepository baseRepository) : base(context, baseRepository)
        {
        }

        public CreateBranchModel GetModelForEditingBranch(int id = 0)
        {
            var model = BaseRepository.FindEntity<Branch>(id);
            var specialities = BaseRepository.QueryWithNoTracking<Specialty>(x => x.Branch)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name
                                            },
                                            IsSelected = x.Branch.Id == id
                                        })
                                        .ToList();
            var result = new CreateBranchModel
            {
                Model = model,
                Specialities = new CustomCheckBoxList(specialities)
            };
            return result;
        }

        public Branch CreateOrUpdateBranch(CreateBranchModel model)
        {
            var result = BaseRepository.ToEntity(model.Model);

            var specIds = model.Specialities != null && model.Specialities.Items != null
                ? model.Specialities.Items.Where(x => x.IsSelected).Select(x => x.Model.Id).ToList()
                : new List<int>();
            var specs = BaseRepository.Query<Specialty>()
                                        .Where(x => specIds.Contains(x.Id))
                                        .ToList();
            BaseRepository.AssignCollection(result, x => x.Specialties, specs);

            Commit();
            return result;
        }

        public CreateContainerModel GetModelForEditingContainer(int id = 0)
        {
            var model = BaseRepository.FindEntity<Container>(id);
            var lessons = BaseRepository.QueryWithNoTracking<Lesson>(x => x.Containers)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name
                                            },
                                            IsSelected = x.Containers.Any(b => b.Id == id)
                                        })
                                        .ToList();
            var groups = BaseRepository.QueryWithNoTracking<Group>(x => x.Containers)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.GroupNumber
                                            },
                                            IsSelected = x.Containers.Any(b => b.Id == id)
                                        })
                                        .ToList();
            var result = new CreateContainerModel
            {
                Model = model,
                Lessons = new CustomRadioButtonList(lessons),
                Groups = new CustomRadioButtonList(groups)
            };
            return result;
        }

        public Container CreateOrUpdateContainer(CreateContainerModel model)
        {
            var result = BaseRepository.ToEntity(model.Model);

            if (model.Groups != null && model.Groups.SelectedValue != 0)
            {
                var selectedGroup = BaseRepository.FindEntity<Group>(model.Groups.SelectedValue);
                BaseRepository.Assign(result, x => x.Group, selectedGroup);
            }
            else
            {
                BaseRepository.Assign(result, x => x.Group, null);
            }

            if (model.Lessons != null && model.Lessons.SelectedValue != 0)
            {
                var selectedLesson = BaseRepository.FindEntity<Lesson>(model.Lessons.SelectedValue);
                BaseRepository.Assign(result, x => x.Lesson, selectedLesson);
            }
            else
            {
                BaseRepository.Assign(result, x => x.Lesson, null);
            }

            Commit();
            return result;
        }

        public CreateCourseModel GetModelForEditingCourse(int id = 0)
        {
            var model = BaseRepository.FindEntity<Course>(id);
            var subjects = BaseRepository.QueryWithNoTracking<Subject>(x => x.Courses)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name
                                            },
                                            IsSelected = x.Courses.Any(b => b.Id == id)
                                        })
                                        .ToList();
            var groups = BaseRepository.QueryWithNoTracking<Group>(x => x.Course)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.GroupNumber
                                            },
                                            IsSelected = x.Course.Id == id
                                        })
                                        .ToList();
            var sets = BaseRepository.QueryWithNoTracking<Set>(x => x.Courses)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name
                                            },
                                            IsSelected = x.Courses.Any(b => b.Id == id)
                                        })
                                        .ToList();
            var result = new CreateCourseModel
            {
                Model = model,
                Groups = new CustomCheckBoxList(groups),
                Subjects = new CustomCheckBoxList(subjects),
                Sets = new CustomRadioButtonList(sets)
            };
            return result;
        }

        public Course CreateOrUpdateCourse(CreateCourseModel model)
        {
            var result = BaseRepository.ToEntity(model.Model);

            var subjIds = model.Subjects != null && model.Subjects.Items != null
                ? model.Subjects.Items.Where(x => x.IsSelected).Select(x => x.Model.Id).ToList()
                : new List<int>();
            var subjects = BaseRepository.Query<Subject>()
                                        .Where(x => subjIds.Contains(x.Id))
                                        .ToList();
            BaseRepository.AssignCollection(result, x => x.Subjects, subjects);

            var groupsIds = model.Groups != null && model.Groups.Items != null
                ? model.Groups.Items.Where(x => x.IsSelected).Select(x => x.Model.Id).ToList()
                : new List<int>();
            var groups = BaseRepository.Query<Group>()
                                        .Where(x => groupsIds.Contains(x.Id))
                                        .ToList();
            BaseRepository.AssignCollection(result, x => x.Groups, groups);

            if (model.Sets != null && model.Sets.SelectedValue != 0)
            {
                var selectedSet = BaseRepository.FindEntity<Set>(model.Sets.SelectedValue);
                BaseRepository.Assign(result, x => x.Set, selectedSet);
            }
            else
            {
                BaseRepository.Assign(result, x => x.Set, null);
            }

            Commit();
            return result;
        }

        public CreateGroupModel GetModelForEditingGroup(int id = 0)
        {
            var model = BaseRepository.FindEntity<Group>(id);
            var students = BaseRepository.QueryWithNoTracking<Student>(x => x.Group)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Fio
                                            },
                                            IsSelected = x.Group.Id == id
                                        })
                                        .ToList();
            var containers = BaseRepository.QueryWithNoTracking<Container>(x => x.Group)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.PairNumber + " " + x.Room
                                            },
                                            IsSelected = x.Group.Id == id
                                        })
                                        .ToList();
            var courses = BaseRepository.QueryWithNoTracking<Course>(x => x.Groups)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Number.ToString()
                                            },
                                            IsSelected = x.Groups.Any(b => b.Id == id)
                                        })
                                        .ToList();
            var teachers = BaseRepository.QueryWithNoTracking<Teacher>(x => x.Groups)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Fio
                                            },
                                            IsSelected = x.Groups.Any(b => b.Id == id)
                                        })
                                        .ToList();
            var result = new CreateGroupModel
            {
                Model = model,
                Students = new CustomCheckBoxList(students),
                Teachers = new CustomRadioButtonList(teachers),
                Courses = new CustomRadioButtonList(courses),
                Containers = new CustomCheckBoxList(containers)
            };
            return result;
        }

        public Group CreateOrUpdateGroup(CreateGroupModel model)
        {
            var result = BaseRepository.ToEntity(model.Model);

            var studentIds = model.Students != null && model.Students.Items != null
                ? model.Students.Items.Where(x => x.IsSelected).Select(x => x.Model.Id).ToList()
                : new List<int>();
            var students = BaseRepository.Query<Student>()
                                        .Where(x => studentIds.Contains(x.Id))
                                        .ToList();
            BaseRepository.AssignCollection(result, x => x.Students, students);

            var containerIds = model.Containers != null && model.Containers.Items != null
                ? model.Containers.Items.Where(x => x.IsSelected).Select(x => x.Model.Id).ToList()
                : new List<int>();
            var containers = BaseRepository.Query<Container>()
                                        .Where(x => containerIds.Contains(x.Id))
                                        .ToList();
            BaseRepository.AssignCollection(result, x => x.Containers, containers);
            
            if (model.Teachers != null && model.Teachers.SelectedValue != 0)
            {
                var selectedTeacher = BaseRepository.FindEntity<Teacher>(model.Teachers.SelectedValue);
                BaseRepository.Assign(result, x => x.Teacher, selectedTeacher);
            }
            else
            {
                BaseRepository.Assign(result, x => x.Teacher, null);
            }

            if (model.Courses != null && model.Courses.SelectedValue != 0)
            {
                var selectedCourse = BaseRepository.FindEntity<Course>(model.Courses.SelectedValue);
                BaseRepository.Assign(result, x => x.Course, selectedCourse);
            }
            else
            {
                BaseRepository.Assign(result, x => x.Course, null);
            }

            Commit();
            return result;
        }

        public CreateLessonModel GetModelForEditingLesson(int id = 0)
        {
            var model = BaseRepository.FindEntity<Lesson>(id);
            var containers = BaseRepository.QueryWithNoTracking<Container>(x => x.Lesson)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.PairNumber.ToString() + " " + x.Room
                                            },
                                            IsSelected = x.Lesson.Id == id
                                        })
                                        .ToList();
            var teachers = BaseRepository.QueryWithNoTracking<Teacher>(x => x.Lessons)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Fio
                                            },
                                            IsSelected = x.Lessons.Any(b => b.Id == id)
                                        })
                                        .ToList();
            var lessonTypes = BaseRepository.QueryWithNoTracking<LessonType>(x => x.Lessons)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name
                                            },
                                            IsSelected = x.Lessons.Any(b => b.Id == id)
                                        })
                                        .ToList();
            var subjects = BaseRepository.QueryWithNoTracking<Subject>(x => x.Lessons)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name
                                            },
                                            IsSelected = x.Lessons.Any(b => b.Id == id)
                                        })
                                        .ToList();
            var result = new CreateLessonModel
            {
                Model = model,
                LessonTypes = new CustomRadioButtonList(lessonTypes),
                Containers = new CustomCheckBoxList(containers),
                Subjects = new CustomRadioButtonList(subjects),
                Teachers = new CustomCheckBoxList(teachers)
            };
            return result;
        }

        public Lesson CreateOrUpdateLesson(CreateLessonModel model)
        {
            var result = BaseRepository.ToEntity(model.Model);

            var containerIds = model.Containers != null && model.Containers.Items != null
                ? model.Containers.Items.Where(x => x.IsSelected).Select(x => x.Model.Id).ToList()
                : new List<int>();
            var containers = BaseRepository.Query<Container>()
                                        .Where(x => containerIds.Contains(x.Id))
                                        .ToList();
            BaseRepository.AssignCollection(result, x => x.Containers, containers);

            var teacherIds = model.Teachers != null && model.Teachers.Items != null
                ? model.Teachers.Items.Where(x => x.IsSelected).Select(x => x.Model.Id).ToList()
                : new List<int>();
            var teachers = BaseRepository.Query<Teacher>()
                                        .Where(x => teacherIds.Contains(x.Id))
                                        .ToList();
            BaseRepository.AssignCollection(result, x => x.Teachers, teachers);

            if (model.LessonTypes != null && model.LessonTypes.SelectedValue != 0)
            {
                var selectedLessonType = BaseRepository.FindEntity<LessonType>(model.LessonTypes.SelectedValue);
                BaseRepository.Assign(result, x => x.LessonType, selectedLessonType);
            }
            else
            {
                BaseRepository.Assign(result, x => x.LessonType, null);
            }

            if (model.Subjects != null && model.Subjects.SelectedValue != 0)
            {
                var selectedSubject = BaseRepository.FindEntity<Subject>(model.Subjects.SelectedValue);
                BaseRepository.Assign(result, x => x.Subject, selectedSubject);
            }
            else
            {
                BaseRepository.Assign(result, x => x.Subject, null);
            }

            Commit();
            return result;
        }

        public CreateLessonTypeModel GetModelForEditingLessonType(int id = 0)
        {
            var model = BaseRepository.FindEntity<LessonType>(id);
            var lessons = BaseRepository.QueryWithNoTracking<Lesson>(x => x.LessonType)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name
                                            },
                                            IsSelected = x.LessonType.Id == id
                                        })
                                        .ToList();
            var result = new CreateLessonTypeModel
            {
                Model = model,
                Lessons = new CustomCheckBoxList(lessons)
            };
            return result;
        }

        public LessonType CreateOrUpdateLessonType(CreateLessonTypeModel model)
        {
            var result = BaseRepository.ToEntity(model.Model);

            var lessonsIds = model.Lessons != null && model.Lessons.Items != null
                ? model.Lessons.Items.Where(x => x.IsSelected).Select(x => x.Model.Id).ToList()
                : new List<int>();
            var lessons = BaseRepository.Query<Lesson>()
                                        .Where(x => lessonsIds.Contains(x.Id))
                                        .ToList();
            BaseRepository.AssignCollection(result, x => x.Lessons, lessons);

            Commit();
            return result;
        }

        public CreateSetModel GetModelForEditingSet(int id = 0)
        {
            var model = BaseRepository.FindEntity<Set>(id);
            var specialties = BaseRepository.QueryWithNoTracking<Specialty>(x => x.Sets)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name
                                            },
                                            IsSelected = x.Sets.Any(s => s.Id == id)
                                        })
                                        .ToList();
            var courses = BaseRepository.QueryWithNoTracking<Course>(x => x.Set)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Number.ToString()
                                            },
                                            IsSelected = x.Set.Id == id
                                        })
                                        .ToList();
            var result = new CreateSetModel
            {
                Model = model,
                Courses = new CustomCheckBoxList(courses),
                Specialties = new CustomRadioButtonList(specialties)
            };
            return result;
        }

        public Set CreateOrUpdateSet(CreateSetModel model)
        {
            var result = BaseRepository.ToEntity(model.Model);

            var courseIds = model.Courses != null && model.Courses.Items != null
                ? model.Courses.Items.Where(x => x.IsSelected).Select(x => x.Model.Id).ToList()
                : new List<int>();
            var courses = BaseRepository.Query<Course>()
                                        .Where(x => courseIds.Contains(x.Id))
                                        .ToList();
            BaseRepository.AssignCollection(result, x => x.Courses, courses);

            if (model.Specialties != null && model.Specialties.SelectedValue != 0)
            {
                var selectedSpecialty = BaseRepository.FindEntity<Specialty>(model.Specialties.SelectedValue);
                BaseRepository.Assign(result, x => x.Specialty, selectedSpecialty);
            }
            else
            {
                BaseRepository.Assign(result, x => x.Specialty, null);
            }

            Commit();
            return result;
        }

        public CreateSpecialtyModel GetModelForEditingSpecialty(int id = 0)
        {
            var model = BaseRepository.FindEntity<Specialty>(id);
            var branches = BaseRepository.QueryWithNoTracking<Branch>(x => x.Specialties)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name
                                            },
                                            IsSelected = x.Specialties.Any(s => s.Id == id)
                                        })
                                        .ToList();
            var sets = BaseRepository.QueryWithNoTracking<Set>(x => x.Specialty)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name
                                            },
                                            IsSelected = x.Specialty.Id == id
                                        })
                                        .ToList();
            var result = new CreateSpecialtyModel
            {
                Model = model,
                Branches = new CustomRadioButtonList(branches),
                Sets = new CustomCheckBoxList(sets)
            };
            return result;
        }

        public Specialty CreateOrUpdateSpecialty(CreateSpecialtyModel model)
        {
            var result = BaseRepository.ToEntity(model.Model);

            var setIds = model.Sets != null && model.Sets.Items != null
                ? model.Sets.Items.Where(x => x.IsSelected).Select(x => x.Model.Id).ToList()
                : new List<int>();
            var sets = BaseRepository.Query<Set>()
                                        .Where(x => setIds.Contains(x.Id))
                                        .ToList();
            BaseRepository.AssignCollection(result, x => x.Sets, sets);

            if (model.Branches != null && model.Branches.SelectedValue != 0)
            {
                var selectedBranch = BaseRepository.FindEntity<Branch>(model.Branches.SelectedValue);
                BaseRepository.Assign(result, x => x.Branch, selectedBranch);
            }
            else
            {
                BaseRepository.Assign(result, x => x.Branch, null);
            }

            Commit();
            return result;
        }

        public CreateStudentModel GetModelForEditingStudent(int id = 0)
        {
            var model = BaseRepository.FindEntity<Student>(id);
            var groups = BaseRepository.QueryWithNoTracking<Group>(x => x.Students)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.GroupNumber
                                            },
                                            IsSelected = x.Students.Any(b => b.Id == id)
                                        })
                                        .ToList();
            var result = new CreateStudentModel
            {
                Model = model,
                Groups = new CustomRadioButtonList(groups)
            };
            return result;
        }

        public Student CreateOrUpdateStudent(CreateStudentModel model)
        {
            var result = BaseRepository.ToEntity(model.Model);

            if (model.Groups != null && model.Groups.SelectedValue != 0)
            {
                var selectedItem = BaseRepository.FindEntity<Group>(model.Groups.SelectedValue);
                BaseRepository.Assign(result, x => x.Group, selectedItem);
            }
            else
            {
                BaseRepository.Assign(result, x => x.Group, null);
            }

            Commit();

            return result;
        }

        public CreateSubjectModel GetModelForEditingSubject(int id = 0)
        {
            var model = BaseRepository.FindEntity<Subject>(id);
            var courses = BaseRepository.QueryWithNoTracking<Course>(x => x.Subjects)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Number.ToString()
                                            },
                                            IsSelected = x.Subjects.Any(b => b.Id == id)
                                        })
                                        .ToList();
            var lessons = BaseRepository.QueryWithNoTracking<Lesson>(x => x.Subject)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name
                                            },
                                            IsSelected = x.Subject.Id == id
                                        })
                                        .ToList();
            var result = new CreateSubjectModel
            {
                Model = model,
                Courses = new CustomCheckBoxList(courses),
                Lessons = new CustomCheckBoxList(lessons)
            };
            return result;
        }

        public Subject CreateOrUpdateSubject(CreateSubjectModel model)
        {
            var result = BaseRepository.ToEntity(model.Model);

            var coursesIds = model.Courses != null && model.Courses.Items != null
                ? model.Courses.Items.Where(x => x.IsSelected).Select(x => x.Model.Id).ToList()
                : new List<int>();
            var courses = BaseRepository.Query<Course>()
                                        .Where(x => coursesIds.Contains(x.Id))
                                        .ToList();
            BaseRepository.AssignCollection(result, x => x.Courses, courses);

            var lessonsIds = model.Lessons != null && model.Lessons.Items != null
                ? model.Lessons.Items.Where(x => x.IsSelected).Select(x => x.Model.Id).ToList()
                : new List<int>();
            var lessons = BaseRepository.Query<Lesson>()
                                        .Where(x => lessonsIds.Contains(x.Id))
                                        .ToList();
            BaseRepository.AssignCollection(result, x => x.Lessons, lessons);

            Commit();
            return result;
        }

        public CreateTeacherModel GetModelForEditingTeacher(int id = 0)
        {
            var teacher = BaseRepository.FindEntity<Teacher>(id);
            var groups = BaseRepository.QueryWithNoTracking<Group>(x => x.Teacher)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.GroupNumber
                                            },
                                            IsSelected = x.Teacher.Id == id
                                        })
                                        .ToList();
            var lessons = BaseRepository.QueryWithNoTracking<Lesson>(x => x.Teachers)
                                        .Select(x => new ChoosableModel<IdNameModel>
                                        {
                                            Model = new IdNameModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name
                                            },
                                            IsSelected = x.Teachers.Any(b => b.Id == id)
                                        })
                                        .ToList();
            return new CreateTeacherModel
            {
                Model = teacher,
                Groups = new CustomCheckBoxList(groups),
                Lessons = new CustomCheckBoxList(lessons)
            };
        }

        public Teacher CreateOrUpdateTeacher(CreateTeacherModel model)
        {
            var result = BaseRepository.ToEntity(model.Model);

            var groupsIds = model.Groups != null && model.Groups.Items != null
                ? model.Groups.Items.Where(x => x.IsSelected).Select(x => x.Model.Id).ToList()
                : new List<int>();
            var groups = BaseRepository.Query<Group>()
                                        .Where(x => groupsIds.Contains(x.Id))
                                        .ToList();
            BaseRepository.AssignCollection(result, x => x.Groups, groups);

            var lessonIds = model.Lessons != null && model.Lessons.Items != null
                ? model.Lessons.Items.Where(x => x.IsSelected).Select(x => x.Model.Id).ToList()
                : new List<int>();
            var lessons = BaseRepository.Query<Lesson>()
                                        .Where(x => lessonIds.Contains(x.Id))
                                        .ToList();
            BaseRepository.AssignCollection(result, x => x.Lessons, lessons);

            Commit();
            return result;
        }
    }
}
