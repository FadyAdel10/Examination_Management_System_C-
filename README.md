# Examination Management System (C# Console Application)

## Overview

The **Examination Management System** is a console-based application developed in **C#** that simulates the lifecycle of an academic exam.

The system supports multiple question types, different exam modes, automatic grading, and student notifications using an **event-driven model**.

The goal of this project is to demonstrate advanced **Object-Oriented Programming (OOP)** concepts in C#, including:

- Inheritance
- Polymorphism
- Generics with constraints
- Event-driven programming
- Deep cloning techniques

The system was designed to follow **clean architecture principles**, emphasizing extensibility, encapsulation, and separation of responsibilities.

---

# Key Features

## Multiple Question Types

The system supports different question formats through a **polymorphic question hierarchy**.

### Supported question types

- True / False Questions
- Choose One Question
- Choose All Question

All question types inherit from the abstract base class **`Question`** and override their specific behaviors.


Each question type implements:

- `Display()`
- `CheckAnswer()`
- `Clone()`

This allows the exam engine to interact with questions without knowing their concrete type, enabling **runtime polymorphism**.

---

# Question Design

The **Question** class represents the core abstraction of the system.

### Main Responsibilities

- Store question metadata
- Store possible answers
- Store correct answers
- Validate input data
- Provide answer validation logic
- Support cloning

---

# Important Design Decisions

## Input Validation

The constructor ensures that invalid questions cannot be created:

- Marks must be greater than zero
- Header and Body must not be null

This prevents invalid states in the system.

---

## Deep Copy Implementation

The class implements deep copying using:

- Copy constructors
- `ICloneable`

Example:

```csharp
Answers = new AnswerList(secondQuestion.Answers);
CorrectAnswer = new AnswerList(secondQuestion.CorrectAnswer);
```
## Efficient String Construction

ToString() uses StringBuilder instead of string concatenation.

```csharp
StringBuilder question = new StringBuilder(...)
```

##Structural Equality

The Equals() method checks:

- Question type

- Header

- Body

- Marks

- Answers

- Correct answers

Two questions are considered equal only if their full structure matches.

# AnswerList

Instead of using List<T>, a custom dynamic array implementation was created.

## Responsibilities

- Store answers using an internal array

- Automatically resize when capacity is reached

- Provide indexed access

- Support searching by answer ID

```csharp
public void Add(Answer answer)
{
    if(Count == answers.Length)
        Array.Resize(ref answers, answers.Length * 2);

    answers[Count++] = answer;
}
```

# Exam System

The Exam class represents the core exam engine.

## Responsibilities

- Manage exam lifecycle

- Store questions

- Record student answers

- Calculate final grade

- Notify students when the exam starts

## Start Phase

When the exam starts:

- Mode changes to Starting

- Students are notified

- An event is triggered

  ```csharp
  public virtual void Start()
  {
    Mode = ExamMode.Starting;
    Subject.NotifyStudents(this);
    OnExamStarted(new ExamEventArgs(Subject,this));
  }
  ```

# Event-Driven Student Notification

The system uses events and delegates to notify students when an exam starts.

## Event Flow

1- Exam starts

2- ExamStarted event is triggered

3- All students subscribed to the event receive the notification

```csharp
public void OnExamStarted(object sender, ExamEventArgs e)
{
    Console.WriteLine($"Dear {Name}, {e.Subject.Name} exam will be start");
}
```

# Student & Subject Model
## Student

Represents a system user who can receive exam notifications.

Responsibilities

- Store student information

- Handle exam start events

## Subject
Represents an academic course.

Responsibilities

- Manage enrolled students

- Register students to exam events

- Notify students when exams start

## Example Program Flow

1- Create a Subject

2- Enroll students

3- Create questions

4- Create an exam

5- Start the exam

6- Students answer questions

7- Exam is corrected automatically

8- Results are displayed

## Technologies Used

- C#

- .NET Console Application

- Object-Oriented Programming

- Events & Delegates

- Generics

- Custom Data Structures

- Deep Copying Techniques
