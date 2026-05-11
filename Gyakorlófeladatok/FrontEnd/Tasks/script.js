// Base URL of the backend API
const BASE_URL = "https://localhost:7194/api";

// If you use the second backend solution, set USE_API_KEY to true
const USE_API_KEY = false;

// API key for the second backend solution
const API_KEY = "school-secret-2026";

// --------------------
// COMMON HELPERS
// --------------------

function getHeaders() {
    const headers = {
        "Content-Type": "application/json"
    };

    if (USE_API_KEY) {
        headers["X-API-KEY"] = API_KEY;
    }

    return headers;
}

function showMessage(text, isError = false) {
    const messageBox = document.getElementById("messageBox");
    messageBox.textContent = text;
    messageBox.className = "message-box";

    if (isError) {
        messageBox.classList.add("message-error");
    } else {
        messageBox.classList.add("message-success");
    }
}

function clearMessage() {
    const messageBox = document.getElementById("messageBox");
    messageBox.textContent = "";
    messageBox.className = "message-box";
}

function formatDate(dateString) {
    if (!dateString) return "";
    return dateString.substring(0, 10);
}

// --------------------
// STUDENT FORM HELPERS
// --------------------

function getStudentFromForm() {
    return {
        id: Number(document.getElementById("studentId").value),
        name: document.getElementById("studentName").value,
        email: document.getElementById("studentEmail").value,
        enrollmentDate: document.getElementById("studentEnrollmentDate").value,
        isActive: document.getElementById("studentIsActive").checked
    };
}

function fillStudentForm(student) {
    document.getElementById("studentId").value = student.id;
    document.getElementById("studentName").value = student.name;
    document.getElementById("studentEmail").value = student.email;
    document.getElementById("studentEnrollmentDate").value = formatDate(student.enrollmentDate);
    document.getElementById("studentIsActive").checked = student.isActive;
}

function clearStudentForm() {
    document.getElementById("studentId").value = "";
    document.getElementById("studentName").value = "";
    document.getElementById("studentEmail").value = "";
    document.getElementById("studentEnrollmentDate").value = "";
    document.getElementById("studentIsActive").checked = false;
}

// --------------------
// COURSE FORM HELPERS
// --------------------

function getCourseFromForm() {
    return {
        id: Number(document.getElementById("courseId").value),
        title: document.getElementById("courseTitle").value,
        category: document.getElementById("courseCategory").value,
        credit: Number(document.getElementById("courseCredit").value),
        isOnline: document.getElementById("courseIsOnline").checked
    };
}

function fillCourseForm(course) {
    document.getElementById("courseId").value = course.id;
    document.getElementById("courseTitle").value = course.title;
    document.getElementById("courseCategory").value = course.category;
    document.getElementById("courseCredit").value = course.credit;
    document.getElementById("courseIsOnline").checked = course.isOnline;
}

function clearCourseForm() {
    document.getElementById("courseId").value = "";
    document.getElementById("courseTitle").value = "";
    document.getElementById("courseCategory").value = "";
    document.getElementById("courseCredit").value = "";
    document.getElementById("courseIsOnline").checked = false;
}

// --------------------
// STUDENT CRUD
// --------------------

async function loadStudents() {
    clearMessage();

    try {
        const response = await fetch(`${BASE_URL}/student`, {
            method: "GET",
            headers: USE_API_KEY ? { "X-API-KEY": API_KEY } : {}
        });

        if (!response.ok) {
            throw new Error("Failed to load students.");
        }

        const students = await response.json();
        renderStudentTable(students);
        showMessage("Students loaded successfully.");
    } catch (error) {
        showMessage(error.message, true);
    }
}

function renderStudentTable(students) {
    const tbody = document.getElementById("studentTableBody");
    tbody.innerHTML = "";

    for (const student of students) {
        const row = document.createElement("tr");

        row.innerHTML = `
            <td>${student.id}</td>
            <td>${student.name}</td>
            <td>${student.email}</td>
            <td>${formatDate(student.enrollmentDate)}</td>
            <td>${student.isActive ? "Yes" : "No"}</td>
            <td>
                <div class="action-buttons">
                    <button onclick="selectStudent(${student.id})">Edit</button>
                    <button class="delete-btn" onclick="deleteStudent(${student.id})">Delete</button>
                </div>
            </td>
        `;

        tbody.appendChild(row);
    }
}

async function getStudentById() {
    clearMessage();

    const id = Number(document.getElementById("searchStudentId").value);

    if (!id) {
        showMessage("Please enter a student id.", true);
        return;
    }

    try {
        const response = await fetch(`${BASE_URL}/student/${id}`, {
            method: "GET",
            headers: USE_API_KEY ? { "X-API-KEY": API_KEY } : {}
        });

        if (!response.ok) {
            throw new Error("Student not found.");
        }

        const student = await response.json();

        document.getElementById("studentSearchResult").innerHTML = `
            <strong>Id:</strong> ${student.id}<br>
            <strong>Name:</strong> ${student.name}<br>
            <strong>Email:</strong> ${student.email}<br>
            <strong>Enrollment Date:</strong> ${formatDate(student.enrollmentDate)}<br>
            <strong>Active:</strong> ${student.isActive ? "Yes" : "No"}
        `;

        showMessage("Student loaded successfully.");
    } catch (error) {
        document.getElementById("studentSearchResult").innerHTML = "";
        showMessage(error.message, true);
    }
}

async function createStudent() {
    clearMessage();

    try {
        const student = getStudentFromForm();
        student.id = 0;

        const response = await fetch(`${BASE_URL}/student`, {
            method: "POST",
            headers: getHeaders(),
            body: JSON.stringify(student)
        });

        if (!response.ok) {
            throw new Error("Failed to create student.");
        }

        clearStudentForm();
        await loadStudents();
        showMessage("Student created successfully.");
    } catch (error) {
        showMessage(error.message, true);
    }
}

async function updateStudent() {
    clearMessage();

    try {
        const student = getStudentFromForm();

        if (!student.id) {
            showMessage("Please enter a student id for update.", true);
            return;
        }

        const response = await fetch(`${BASE_URL}/student`, {
            method: "PUT",
            headers: getHeaders(),
            body: JSON.stringify(student)
        });

        if (!response.ok) {
            throw new Error("Failed to update student.");
        }

        clearStudentForm();
        await loadStudents();
        showMessage("Student updated successfully.");
    } catch (error) {
        showMessage(error.message, true);
    }
}

async function deleteStudent(id) {
    clearMessage();

    const confirmed = confirm("Are you sure you want to delete this student?");
    if (!confirmed) {
        return;
    }

    try {
        const response = await fetch(`${BASE_URL}/student/${id}`, {
            method: "DELETE",
            headers: USE_API_KEY ? { "X-API-KEY": API_KEY } : {}
        });

        if (!response.ok) {
            throw new Error("Failed to delete student.");
        }

        await loadStudents();
        showMessage("Student deleted successfully.");
    } catch (error) {
        showMessage(error.message, true);
    }
}

async function selectStudent(id) {
    clearMessage();

    try {
        const response = await fetch(`${BASE_URL}/student/${id}`, {
            method: "GET",
            headers: USE_API_KEY ? { "X-API-KEY": API_KEY } : {}
        });

        if (!response.ok) {
            throw new Error("Failed to load student.");
        }

        const student = await response.json();
        fillStudentForm(student);
        showMessage("Student loaded into form.");
    } catch (error) {
        showMessage(error.message, true);
    }
}

// --------------------
// COURSE CRUD
// --------------------

async function loadCourses() {
    clearMessage();

    try {
        const response = await fetch(`${BASE_URL}/course`, {
            method: "GET",
            headers: USE_API_KEY ? { "X-API-KEY": API_KEY } : {}
        });

        if (!response.ok) {
            throw new Error("Failed to load courses.");
        }

        const courses = await response.json();
        renderCourseTable(courses);
        showMessage("Courses loaded successfully.");
    } catch (error) {
        showMessage(error.message, true);
    }
}

function renderCourseTable(courses) {
    const tbody = document.getElementById("courseTableBody");
    tbody.innerHTML = "";

    for (const course of courses) {
        const row = document.createElement("tr");

        row.innerHTML = `
            <td>${course.id}</td>
            <td>${course.title}</td>
            <td>${course.category}</td>
            <td>${course.credit}</td>
            <td>${course.isOnline ? "Yes" : "No"}</td>
            <td>
                <div class="action-buttons">
                    <button onclick="selectCourse(${course.id})">Edit</button>
                    <button class="delete-btn" onclick="deleteCourse(${course.id})">Delete</button>
                </div>
            </td>
        `;

        tbody.appendChild(row);
    }
}

async function getCourseById() {
    clearMessage();

    const id = Number(document.getElementById("searchCourseId").value);

    if (!id) {
        showMessage("Please enter a course id.", true);
        return;
    }

    try {
        const response = await fetch(`${BASE_URL}/course/${id}`, {
            method: "GET",
            headers: USE_API_KEY ? { "X-API-KEY": API_KEY } : {}
        });

        if (!response.ok) {
            throw new Error("Course not found.");
        }

        const course = await response.json();

        document.getElementById("courseSearchResult").innerHTML = `
            <strong>Id:</strong> ${course.id}<br>
            <strong>Title:</strong> ${course.title}<br>
            <strong>Category:</strong> ${course.category}<br>
            <strong>Credit:</strong> ${course.credit}<br>
            <strong>Online:</strong> ${course.isOnline ? "Yes" : "No"}
        `;

        showMessage("Course loaded successfully.");
    } catch (error) {
        document.getElementById("courseSearchResult").innerHTML = "";
        showMessage(error.message, true);
    }
}

async function createCourse() {
    clearMessage();

    try {
        const course = getCourseFromForm();
        course.id = 0;

        const response = await fetch(`${BASE_URL}/course`, {
            method: "POST",
            headers: getHeaders(),
            body: JSON.stringify(course)
        });

        if (!response.ok) {
            throw new Error("Failed to create course.");
        }

        clearCourseForm();
        await loadCourses();
        showMessage("Course created successfully.");
    } catch (error) {
        showMessage(error.message, true);
    }
}

async function updateCourse() {
    clearMessage();

    try {
        const course = getCourseFromForm();

        if (!course.id) {
            showMessage("Please enter a course id for update.", true);
            return;
        }

        const response = await fetch(`${BASE_URL}/course`, {
            method: "PUT",
            headers: getHeaders(),
            body: JSON.stringify(course)
        });

        if (!response.ok) {
            throw new Error("Failed to update course.");
        }

        clearCourseForm();
        await loadCourses();
        showMessage("Course updated successfully.");
    } catch (error) {
        showMessage(error.message, true);
    }
}

async function deleteCourse(id) {
    clearMessage();

    const confirmed = confirm("Are you sure you want to delete this course?");
    if (!confirmed) {
        return;
    }

    try {
        const response = await fetch(`${BASE_URL}/course/${id}`, {
            method: "DELETE",
            headers: USE_API_KEY ? { "X-API-KEY": API_KEY } : {}
        });

        if (!response.ok) {
            throw new Error("Failed to delete course.");
        }

        await loadCourses();
        showMessage("Course deleted successfully.");
    } catch (error) {
        showMessage(error.message, true);
    }
}

async function selectCourse(id) {
    clearMessage();

    try {
        const response = await fetch(`${BASE_URL}/course/${id}`, {
            method: "GET",
            headers: USE_API_KEY ? { "X-API-KEY": API_KEY } : {}
        });

        if (!response.ok) {
            throw new Error("Failed to load course.");
        }

        const course = await response.json();
        fillCourseForm(course);
        showMessage("Course loaded into form.");
    } catch (error) {
        showMessage(error.message, true);
    }
}

// --------------------
// EVENT LISTENERS
// --------------------

document.getElementById("createStudentBtn").addEventListener("click", createStudent);
document.getElementById("updateStudentBtn").addEventListener("click", updateStudent);
document.getElementById("loadStudentsBtn").addEventListener("click", loadStudents);
document.getElementById("clearStudentBtn").addEventListener("click", clearStudentForm);
document.getElementById("searchStudentBtn").addEventListener("click", getStudentById);

document.getElementById("createCourseBtn").addEventListener("click", createCourse);
document.getElementById("updateCourseBtn").addEventListener("click", updateCourse);
document.getElementById("loadCoursesBtn").addEventListener("click", loadCourses);
document.getElementById("clearCourseBtn").addEventListener("click", clearCourseForm);
document.getElementById("searchCourseBtn").addEventListener("click", getCourseById);

// Initial load
window.addEventListener("DOMContentLoaded", async () => {
    await loadStudents();
    await loadCourses();
});