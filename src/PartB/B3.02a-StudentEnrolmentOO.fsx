module rec EnrollmentOO =
// ---->^  The "rec" means the definitions in this file can be out of order
// In this case, you must highlight the entire file to evalulate the code

// =================================
// Exercise 2: Student Enrolment
// =================================

// Using the requirements in the exercises.txt for Student Enrolment

// 1. Determine all the classes
// 2. Determine the responsibilities for each class
// 3. Define interface types for each class (no implementation needed)

//============================================
// Your code starts here
// Indent everything one tab so that the "module rec" above works
//============================================
    type undefined = exn

    type StudentId = string // for now

    type Student =
        abstract member StudentId : StudentId
        // what else?

    type CourseId = string // for now

    type Course =
        abstract member CourseId : CourseId
        // what else?

    type Grade = int
    type GPA = float // for now

    type Enrolment =
      undefined
        // what else?

    type Teacher =
        // we dont have any details
        interface end


(*
Questions:

* In a sucessful Enrolment, the Course needs to increment the number of students enrolled
  Should it contain a list of students, or just a count of the number enrolled so far?

* The Grade of an Enrolment is available even if a grade has not been given yet. How to fix this?

* In CalculateGPA for a Student, where does the Student get all the Enrolments from?
* In CalculateGPA for a Course, where does the Course get all the Enrolments from?

* How is all this data stored? There is no mention of a database!
  For example: Should the Course store the list of enrolled Students inside itself,
  or fetch them from a database?


*)

// =================================
// Extra time? Add a new requirement!
// =================================

    (*
    * There are two types of Students now: Regular and Auditor
    * An Auditor ("listener") can enrol in a Course like a Regular student
      BUT they are not graded, and they are excluded from the GPA calculations.
    *)

    // First remove CalculateGPA from Student -- it is not applicable to the base class

    type StudentType = Regular | Audit

    // Define a Regular student as inheriting from Student
    // with the addition of CalculateGPA
    type RegularStudent =
        inherit Student
        // what else?

    // Define a Auditor student as inheriting from Student
    // without the addition of CalculateGPA
    type AuditorStudent =
        inherit Student
        // what else?


(*
Questions

* Enrolment still has a SetGrade method even though it is not applicable for Auditors.
  What is the best way to avoid it being accidentally called for an Auditor enrolment?

* When the class calculates the GPA for the students, it must check the type of each student
  and ignore the Auditors.
  The Course is now coupled to knowing about Auditors.
  Is there a nicer way to do this, so that the class does not know about Auditors?

*)