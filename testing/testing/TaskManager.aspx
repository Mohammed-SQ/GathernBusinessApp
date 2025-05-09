<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskManager.aspx.cs" Inherits="testing.TaskManager" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Task Manager</title>
    <link rel="stylesheet" href="Styles.css" />
    <style>
        body {
            margin: 0;
            font-family: Arial, sans-serif;
            display: flex;
            height: 100vh;
        }

        /* Left Side Styling */
        .left-side {
            width: 35%;
            background-color: #fce4ec;
            background-image: url('background.gif');
            background-size: cover;
            background-repeat: no-repeat;
            padding: 20px;
            display: flex;
            flex-direction: column;
            justify-content: flex-start;
            align-items: center;
        }

        .left-side h1 {
            margin: 10px 0;
            font-size: 2rem;
            color: #d81b60;
        }

        .left-side p {
            font-size: 1.2rem;
            margin: 10px 0;
            color: #4a4a4a;
        }

        .left-side .status {
            margin-top: auto;
            width: 100%;
            padding: 20px;
            background-color: rgba(255, 255, 255, 0.8);
            border-radius: 10px;
        }

        .left-side .status p {
            margin: 5px 0;
        }

        /* Right Side Styling */
        .right-side {
            flex-grow: 1;
            background-color: #ffebf0;
            display: flex;
            flex-direction: column;
        }

        .tabs-menu {
            display: flex;
            background-color: #f8c8d4;
            padding: 10px;
            border-bottom: 2px solid #eeb2c4;
        }

        .tabs-menu a {
            flex: 1;
            text-align: center;
            text-decoration: none;
            color: #d81b60;
            font-weight: bold;
            padding: 10px;
            cursor: pointer;
        }

        .tabs-menu a.active {
            border-bottom: 2px solid #f50057;
        }

        .content {
            flex-grow: 1;
            padding: 20px;
        }

        .todo-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 20px;
        }

        .todo-header h2 {
            font-size: 1.5rem;
            color: #d81b60;
        }

        .todo-header .buttons button {
            background-color: #007bff;
            color: #fff;
            border: none;
            padding: 10px 15px;
            font-size: 1rem;
            cursor: pointer;
            border-radius: 5px;
            margin-left: 10px;
        }

        .todo-header .buttons button:hover {
            background-color: #0056b3;
        }

        .tasks-container {
            display: flex;
            flex-direction: column;
            gap: 10px;
        }

        .task-card {
            background: #fff;
            border: 1px solid #e0e0e0;
            padding: 15px;
            border-radius: 5px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .task-card input[type="checkbox"] {
            margin-right: 10px;
        }

        .task-card .details {
            flex-grow: 1;
            padding-left: 10px;
        }
    </style>
</head>
<body>
    <form id="TaskManagerForm" runat="server">
        <div class="left-side">
            <h1>Luffy</h1>
            <p>Welcome to Study Buddy!</p>
            <div class="status">
                <p><strong>MOOD:</strong> I'm stressed. You have overdue tasks... please complete them :(</p>
                <p><strong>WEIGHT:</strong> 2/3 of a medium radish</p>
            </div>
        </div>

        <div class="right-side">
            <!-- Tabs Menu -->
            <div class="tabs-menu">
                <a href="#tasks" class="active">Tasks</a>
                <a href="#calendar">Calendar</a>
                <a href="#inventory">Inventory</a>
                <a href="#profile">Profile</a>
            </div>

            <!-- Main Content Area -->
            <div class="content" id="tasks">
                <div class="todo-header">
                    <h2>TO-DO</h2>
                    <div class="buttons">
                        <button onclick="openFilter()">Filter</button>
                        <button onclick="openCreateTaskModal()">+ Create Task</button>
                    </div>
                </div>
                <div class="tasks-container">
                    <div class="task-card">
                        <input type="checkbox">
                        <div class="details">
                            <p><strong>Homework</strong></p>
                            <p>Programming II homework</p>
                            <p>Due: 2 days ago</p>
                        </div>
                        <button>X</button>
                    </div>
                </div>
            </div>

            <div class="content" id="calendar" style="display: none;">
                <h2>Calendar</h2>
                <p>Calendar functionality will be implemented here.</p>
            </div>

            <div class="content" id="inventory" style="display: none;">
                <h2>Inventory</h2>
                <p>No items yet.</p>
            </div>

            <div class="content" id="profile" style="display: none;">
                <h2>Profile</h2>
                <p>Edit your name and title here.</p>
            </div>
        </div>
    </form>
    <script>
        const tabs = document.querySelectorAll('.tabs-menu a');
        const contents = document.querySelectorAll('.content');

        tabs.forEach(tab => {
            tab.addEventListener('click', function (e) {
                e.preventDefault();

                tabs.forEach(t => t.classList.remove('active'));
                this.classList.add('active');

                const target = this.getAttribute('href').substring(1);

                contents.forEach(content => {
                    content.style.display = content.id === target ? 'block' : 'none';
                });
            });
        });

        function openFilter() {
            alert('Filter functionality coming soon!');
        }

        function openCreateTaskModal() {
            alert('Create Task modal coming soon!');
        }
    </script>
</body>
</html>
