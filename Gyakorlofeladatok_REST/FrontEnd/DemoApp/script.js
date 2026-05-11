// Base URL of the ASP.NET Core REST API
const API_URL = "https://localhost:7273/api/employee";

// Form elements
const idInput = document.getElementById("id");
const nameInput = document.getElementById("name");
const hiredInput = document.getElementById("hired");
const isActiveInput = document.getElementById("isActive");
const positionInput = document.getElementById("position");
const salaryInput = document.getElementById("salary");

// Other UI elements
const employeeTableBody = document.getElementById("employeeTableBody");
const searchIdInput = document.getElementById("searchId");
const searchResult = document.getElementById("searchResult");
const messageBox = document.getElementById("messageBox");

// Buttons
const loadBtn = document.getElementById("loadBtn");
const createBtn = document.getElementById("createBtn");
const updateBtn = document.getElementById("updateBtn");
const searchBtn = document.getElementById("searchBtn");

// Load data when page opens
document.addEventListener("DOMContentLoaded", async () => {
    await loadEmployees();
});

// Button events
loadBtn.addEventListener("click", async () => {
    await loadEmployees();
});

createBtn.addEventListener("click", async () => {
    await createEmployee();
});

updateBtn.addEventListener("click", async () => {
    await updateEmployee();
});

searchBtn.addEventListener("click", async () => {
    await getEmployeeById();
});

// Show message to the user
function showMessage(text, isError = false) {
    messageBox.textContent = text;
    messageBox.className = "message-box";

    if (isError) {
        messageBox.classList.add("message-error");
    } else {
        messageBox.classList.add("message-success");
    }
}

// Clear form fields
function clearForm() {
    idInput.value = "";
    nameInput.value = "";
    hiredInput.value = "";
    isActiveInput.checked = false;
    positionInput.value = "0";
    salaryInput.value = "";
}

// Convert numeric position value to readable text
function getPositionText(positionValue) {
    switch (positionValue) {
        case 0:
            return "Developer";
        case 1:
            return "Tester";
        case 2:
            return "Manager";
        default:
            return "Unknown";
    }
}

// Read form data and build an employee object
function getEmployeeFromForm() {
    return {
        id: Number(idInput.value),
        name: nameInput.value,
        hired: hiredInput.value,
        isActive: isActiveInput.checked,
        position: Number(positionInput.value),
        salary: Number(salaryInput.value)
    };
}

// Fill form fields from selected employee
function fillForm(employee) {
    idInput.value = employee.id;
    nameInput.value = employee.name;

    // Convert date to yyyy-mm-dd format for input type="date"
    const date = new Date(employee.hired);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, "0");
    const day = String(date.getDate()).padStart(2, "0");
    hiredInput.value = `${year}-${month}-${day}`;

    isActiveInput.checked = employee.isActive;
    positionInput.value = employee.position;
    salaryInput.value = employee.salary;
}

// Load all employees from the API
async function loadEmployees() {
    try {
        const response = await fetch(API_URL);

        if (!response.ok) {
            throw new Error("Failed to load employees.");
        }

        const employees = await response.json();
        renderEmployeeTable(employees);
        showMessage("Employees loaded successfully.");
    } catch (error) {
        showMessage(error.message, true);
    }
}

// Render employee list into the table
function renderEmployeeTable(employees) {
    employeeTableBody.innerHTML = "";

    for (const employee of employees) {
        const row = document.createElement("tr");

        row.innerHTML = `
            <td>${employee.id}</td>
            <td>${employee.name}</td>
            <td>${employee.hired ? employee.hired.substring(0, 10) : ""}</td>
            <td>${employee.isActive ? "Yes" : "No"}</td>
            <td>${getPositionText(employee.position)}</td>
            <td>${employee.salary}</td>
            <td>
                <button class="action-btn" onclick="selectEmployee(${employee.id})">Edit</button>
                <button class="action-btn delete-btn" onclick="deleteEmployee(${employee.id})">Delete</button>
            </td>
        `;

        employeeTableBody.appendChild(row);
    }
}

// Get one employee by id
async function getEmployeeById() {
    const id = Number(searchIdInput.value);

    if (!id) {
        showMessage("Please enter an id.", true);
        return;
    }

    try {
        const response = await fetch(`${API_URL}/${id}`);

        if (!response.ok) {
            throw new Error("Employee not found.");
        }

        const employee = await response.json();

        searchResult.innerHTML = `
            <strong>Id:</strong> ${employee.id}<br>
            <strong>Name:</strong> ${employee.name}<br>
            <strong>Hired:</strong> ${employee.hired.substring(0, 10)}<br>
            <strong>Active:</strong> ${employee.isActive ? "Yes" : "No"}<br>
            <strong>Position:</strong> ${getPositionText(employee.position)}<br>
            <strong>Salary:</strong> ${employee.salary}
        `;

        showMessage("Employee loaded successfully.");
    } catch (error) {
        searchResult.innerHTML = "";
        showMessage(error.message, true);
    }
}

// Create a new employee
async function createEmployee() {
    try {
        const employee = getEmployeeFromForm();

        // Id is usually generated by the database for new records
        employee.id = 0;

        const response = await fetch(API_URL, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(employee)
        });

        if (!response.ok) {
            throw new Error("Failed to create employee.");
        }

        clearForm();
        await loadEmployees();
        showMessage("Employee created successfully.");
    } catch (error) {
        showMessage(error.message, true);
    }
}

// Update an existing employee
async function updateEmployee() {
    try {
        const employee = getEmployeeFromForm();

        if (!employee.id) {
            showMessage("Please enter an id for update.", true);
            return;
        }

        const response = await fetch(API_URL, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(employee)
        });

        if (!response.ok) {
            throw new Error("Failed to update employee.");
        }

        clearForm();
        await loadEmployees();
        showMessage("Employee updated successfully.");
    } catch (error) {
        showMessage(error.message, true);
    }
}

// Delete an employee by id
async function deleteEmployee(id) {
    const confirmed = confirm("Are you sure you want to delete this employee?");

    if (!confirmed) {
        return;
    }

    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: "DELETE"
        });

        if (!response.ok) {
            throw new Error("Failed to delete employee.");
        }

        await loadEmployees();
        showMessage("Employee deleted successfully.");
    } catch (error) {
        showMessage(error.message, true);
    }
}

// Load one employee into the form for editing
async function selectEmployee(id) {
    try {
        const response = await fetch(`${API_URL}/${id}`);

        if (!response.ok) {
            throw new Error("Failed to load employee for editing.");
        }

        const employee = await response.json();
        fillForm(employee);
        showMessage("Employee loaded into form.");
    } catch (error) {
        showMessage(error.message, true);
    }
}