using MvcApp.DalEntities.Entities;
using MvcApp.Models.Create;

namespace MvcApp.BusinessLogic.Managers.Interfaces
{
    public interface ICreateWithAdditionalManager
    {
        CreateBranchModel GetModelForEditingBranch(int id = 0);
        Branch CreateOrUpdateBranch(CreateBranchModel model);

        CreateContainerModel GetModelForEditingContainer(int id = 0);
        Container CreateOrUpdateContainer(CreateContainerModel model);

        CreateCourseModel GetModelForEditingCourse(int id = 0);
        Course CreateOrUpdateCourse(CreateCourseModel model);

        CreateGroupModel GetModelForEditingGroup(int id = 0);
        Group CreateOrUpdateGroup(CreateGroupModel model);

        CreateLessonModel GetModelForEditingLesson(int id = 0);
        Lesson CreateOrUpdateLesson(CreateLessonModel model);

        CreateLessonTypeModel GetModelForEditingLessonType(int id = 0);
        LessonType CreateOrUpdateLessonType(CreateLessonTypeModel model);

        CreateSetModel GetModelForEditingSet(int id = 0);
        Set CreateOrUpdateSet(CreateSetModel model);

        CreateSpecialtyModel GetModelForEditingSpecialty(int id = 0);
        Specialty CreateOrUpdateSpecialty(CreateSpecialtyModel model);

        CreateStudentModel GetModelForEditingStudent(int id = 0);
        Student CreateOrUpdateStudent(CreateStudentModel model);

        CreateSubjectModel GetModelForEditingSubject(int id = 0);
        Subject CreateOrUpdateSubject(CreateSubjectModel model);

        CreateTeacherModel GetModelForEditingTeacher(int id = 0);
        Teacher CreateOrUpdateTeacher(CreateTeacherModel model);
    }
}
