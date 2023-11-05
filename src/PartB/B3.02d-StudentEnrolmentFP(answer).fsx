module rec EnrollmentFP =
// ---->^  The "rec" means the definitions in this file can be out of order
// In this case, you must highlight the entire file to evalulate the code

// =================================
// Exercise 2: Student Enrolment
// =================================

// Using the requirements in the exercises.txt for Student Enrolment

// 1. Determine all the workflows
// 2. Define the inputs and outputs for each workflow

//============================================
// Your code starts here
// Indent everything one tab so that the "module rec" above works
//============================================

    type undefined = exn

    //---------------------------
    // Workflow "Enrol"
    //---------------------------
    type Enrol = Student * Course -> EnrolmentResult

    // Data types arising from this Workflow
    type StudentId = string // for now
    type Student = {
        StudentId : StudentId
    }

    type CourseId = string // for now
    type Course = {
        CourseId : CourseId
        MaxStudents : int
        Teacher : Teacher
    }

    type Teacher = {
        MaxCourses : int
        }
        // we dont have any details except MaxCourses

    type EnrolmentResult =
        | Success of Enrolment
        | NotEligible of IneligibleReason
        | TooManyEnrolledAlready
    type IneligibleReason = string // for now

    type Enrolment = {
        Student : Student
        Course : Course
        Grade : Grade option  // not initially assigned
    }

    type Grade = int

    //---------------------------
    // Workflow "Set Max Students for Course"
    //---------------------------

    type SetMaxStudentsForCourse = int * Course -> Course
        // Course is immutable, so must be returned

    //---------------------------
    // Workflow "Assign Teacher"
    //---------------------------

    type AssignTeacher = Teacher * Course -> AssignTeacherResult


    type AssignTeacherResult =
        | Sucess of Course  // updated Course
        // we dont have any details. Could this fail if the teacher is over worked?


    //---------------------------
    // Workflow "Assign Grade"
    //---------------------------

    type AssignGrade = Enrolment * Grade -> Enrolment
       // enrolment is immutable, so must be returned


    //---------------------------
    // Workflow "Calculate Student GPA"
    //---------------------------

    type CalculateStudentGPA = Student -> GPA
        // where do the enrolments come from? Answer: a helper function
        // the implementation will look like
        (*
        CalculateStudentGPA =
            student
            |> GetEnrolmentsForStudent
            |> CalculateGPA
        *)

    type GetEnrolmentsForStudent = Student * Database -> Enrolment list
       // helper method. We explicitly passing in a database

    type CalculateGPA = Enrolment list -> GPA
       // This can be reused for Courses as well

    type GPA = float // for now

    type Database = undefined // need more info

    //---------------------------
    // Workflow "Calculate Student GPA"
    //---------------------------

    type CalculateCourseGPA = Student -> GPA
        // where do the enrolments come from? Answer: a helper function
        // the implementation will look like
        (*
        CalculateCourseGPA =
            student
            |> GetEnrolmentsForCourse
            |> CalculateGPA
        *)

    type GetEnrolmentsForCourse = Course * Database -> Enrolment list
       // helper method



// =================================
// Extra time? Add a new requirement!
// =================================

    (*
    * There are two types of Students now: Regular and Auditor
    * An Auditor ("listener") can enrol in a Course like a Regular student
      BUT they are not graded, and they are excluded from the GPA calculations.
    *)


    type StudentType = Regular | Audit

    // Define a Regular student as inheriting from Student
    type RegularStudent = {
        StudentId : StudentId
    }
    type AuditorStudent = {
        StudentId : StudentId
    }
    type Student_v2 =
        | Regular of RegularStudent
        | Auditor of AuditorStudent


    type GradedEnrolment = {
        Student : RegularStudent
        Course : Course
        Grade : Grade option  // not initially assigned
    }
    type UngradedEnrolment = {
        Student : AuditorStudent
        Course : Course
    }

    type Enrolment_v2 =
        | Graded of GradedEnrolment
        | Ungraded of UngradedEnrolment




(*
Questions

* Enrolment still has a SetGrade method even though it is not applicable for Auditors.
  What is the best way of it being accidentally called for an Auditor enrolment?
*)

    // Answer: in this design we have different types for graded and ungraded
    // so it can't happen
    type AssignGrade_v2 = GradedEnrolment * Grade -> GradedEnrolment


(*
* When the class calculates the GPA for the students, it must check the type of each student
  and ignore the Auditors.
  The Course is now coupled to knowing about Auditors.
  Is there a nicer way to do this, so that the class does not know about Auditors?
*)

    // Answer: in this design we have different types for graded and ungraded
    // so it can't happen.
    type CalculateGPA_v2 = GradedEnrolment list -> GPA



