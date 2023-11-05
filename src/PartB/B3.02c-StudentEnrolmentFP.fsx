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
    type Enrol = undefined

    // Data types arising from this Workflow
    type StudentId = string // for now
    type Student = {
        StudentId : StudentId
    }

    type CourseId = string // for now
    type Course = {
        SomeField: undefined
    }

    type Teacher = undefined

    type Enrolment = undefined

    type Grade = int

    //---------------------------
    // Workflow "Set Max Students for Course"
    //---------------------------

    type SetMaxStudentsForCourse = undefined

    //---------------------------
    // Workflow "Assign Teacher"
    //---------------------------

    type AssignTeacher = undefined

    //---------------------------
    // Document the remaining Workflows in the same way
    //---------------------------


// =================================
// Extra time? Add a new requirement!
// =================================

    (*
    * There are two types of Students now: Regular and Auditor
    * An Auditor ("listener") can enrol in a Course like a Regular student
      BUT they are not graded, and they are excluded from the GPA calculations.
    *)


    // YOUR CODE HERE
    undefined





(*
Questions

* Enrolment still has a SetGrade method even though it is not applicable for Auditors.
  What is the best way to avoid it being accidentally called for an Auditor enrolment?

* When the class calculates the GPA for the students, it must check the type of each student
  and ignore the Auditors.
  The Course is now coupled to knowing about Auditors.
  Is there a nicer way to do this, so that the class does not know about Auditors?

*)