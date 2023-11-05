(*
======================================
Event Sourced Student Enrolment

Exercise:
* Define the commands and events for the event sourcing system in section 2
* Domain model and state (section 1) are done for you!
* You DONT have to do an implementation!

======================================
*)

// ===============================
// 1. Domain model (DONE)
// ===============================

// The domain is copied over from B3.02d-StudentEnrolmentFP(answer).fsx
// Except: The function types (actions) are removed. They are converted to Commands instead

type undefined = exn

type StudentId = string // for now
type Student = {
    StudentId : StudentId
}

type TeacherId = string // for now
type Teacher = {
    TeacherId : TeacherId
    MaxCourses : int
    }

type CourseId = string // for now
type Course = {
    CourseId : CourseId
    MaxStudents : int
    Teacher : Teacher
}

type Grade = int

type EnrolmentId = string // for now
type Enrolment = {
    EnrolmentId : EnrolmentId
    Student : Student
    Course : Course
    Grade : Grade option  // not initially assigned
}

type IneligibleReason = string // for now

type EnrolmentResult =
    | Success of Enrolment
    | NotEligible of IneligibleReason
    | TooManyEnrolledAlready

type GPA = float // for now

// ====================================
// 2. EXERCISE: Define the events and commands for event sourcing
// ====================================


// These are based on the function types (actions) from previous design

/// A desired action
type EnrolmentSystemCommand =
    // fill these in
    ??


/// An event representing a state change that happened
type EnrolmentSystemEvent =
    // fill these in
    ??






