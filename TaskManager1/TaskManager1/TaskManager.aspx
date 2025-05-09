<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskManager.aspx.cs" Inherits="TaskManager1.TaskManager" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Task Manager</title>
    <link rel="stylesheet" href="Styles.css" />
</head>

<body>
    <form id="TaskManagerForm" runat="server">
        <asp:HiddenField ID="ActiveTabField" runat="server" />

        <div class="container">
            <!-- Left Side -->
            <div class="left-side">
                <div class="welcome-section">
                    <h2>Welcome, <asp:Label ID="lblWelcome" runat="server" Text=""></asp:Label></h2>
                    <div class="background-image">
                        <img src="Images/background.gif" alt="Background" />
                    </div>
                    <div class="info-box">
                        <p class="mood">You have overdue tasks... please complete them :(</p>
                        <p class="mood">Every small step counts... you're doing fantastic! 🌟</p>
                        <p class="mood">Tasks are waiting... let’s complete them together!</p>
                    </div>
                </div>
            </div>

            <!-- Right Side -->
            <div class="right-side">
                <div class="navbar">
                    <ul>
                        <li><a href="#" onclick="setActiveTab('activeTab')">Tasks</a></li>
                        <li><a href="#" onclick="setActiveTab('calendarTab')">Calendar</a></li>
                        <li><a href="#" onclick="setActiveTab('profileTab')">Profile</a></li>
                    </ul>
                </div>

                <!-- Active Tasks Tab -->
                <div id="activeTab" class="tabContent">
                    <div class="tab-header">
                        <h1>TO-DO</h1>
                        <div class="tabs">
                            <button type="button" onclick="setActiveTab('activeTab')">Active</button>
                            <button type="button" onclick="setActiveTab('completedTab')">Completed</button>
                        </div>
                        <div class="actions">
                            <button type="button" class="filter-button" onclick="toggleFilterOptions()">🔍 Filter</button>
                            <button type="button" class="create-task-button" onclick="showCreateTaskModal();">+ Create Task</button>
                        </div>
                    </div>

                    <div class="filter-section" id="filterSection" style="display:none;">
                        <asp:RadioButtonList ID="FilterOptions" runat="server" RepeatDirection="Horizontal" CssClass="filter-options" AutoPostBack="true" OnSelectedIndexChanged="FilterOptions_CheckedChanged">
                            <asp:ListItem Text="Due Date" Value="DueDate" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Priority" Value="Priority"></asp:ListItem>
                            <asp:ListItem Text="Latest" Value="Latest"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

     <asp:GridView ID="ActiveTasksGrid" runat="server" AutoGenerateColumns="False" 
    CssClass="task-grid" ShowHeader="False" OnDataBound="ActiveTasksGrid_DataBound" 
    OnRowCommand="ActiveTasksGrid_RowCommand" DataKeyNames="TaskID">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <div class="checkbox-wrapper">
                    <asp:CheckBox ID="TaskCheckbox" runat="server" CssClass="task-checkbox" 
                        Checked='<%# Convert.ToBoolean(Eval("IsCompleted")) %>' 
                        AutoPostBack="True" OnCheckedChanged="TaskCheckbox_CheckedChanged" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField>
            <ItemTemplate>
                <div class="task-item">
                    <div class="task-content">
                        <div class="task-title">
                            <b><%# Eval("Title") %></b>
                            <span class="priority">Priority: <%# Eval("PriorityText") %></span>
                            <span class="tags">Tags: <%# Eval("Tags") %></span>
                        </div>
                        <div class="task-body"><%# Eval("Description") %></div>
                        <div class="task-footer"><%# GetDueDateMessage(Eval("DueDate")) %></div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="DeleteTaskButton" runat="server" CommandName="DeleteTask" 
                    CommandArgument='<%# Eval("TaskID") %>' CssClass="delete-task-button">
                    &#x2715;
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>


<!-- No Tasks Message -->
<asp:Label ID="noTasksMessage" runat="server" Text="You don't have tasks!" 
    CssClass="no-tasks-message" Visible="false"></asp:Label>


                </div>

                <!-- Completed Tasks Tab -->
                <div id="completedTab" class="tabContent" style="display:none;">
                    <div class="tab-header">
                        <h1>TO-DO</h1>
                        <div class="tabs">
                            <button type="button" onclick="setActiveTab('activeTab')">Active</button>
                            <button type="button" onclick="setActiveTab('completedTab')">Completed</button>
                        </div>
                        <div class="actions">
                            <button type="button" class="filter-button">🔍 Filter</button>
                            <button type="button" class="create-task-button" onclick="showCreateTaskModal();">+ Create Task</button>
                        </div>
                    </div>

                    <asp:Button ID="DeleteAllCompletedButton" runat="server" Text="Delete ALL Completed" CssClass="btn-delete-all" OnClick="DeleteAllCompleted_Click" />

                    <asp:GridView ID="CompletedTasksGrid" runat="server" AutoGenerateColumns="False" CssClass="task-grid" ShowHeader="False" OnRowCommand="CompletedTasksGrid_RowCommand">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="task-card" style="display: flex; justify-content: space-between; align-items: flex-start;">
                                        <div style="flex: 1;">
                                            <div class="task-header" style="margin-bottom: 10px;">
                                                <span class="task-title"><b><%# Eval("Title") %></b></span>
                                                <span class="priority">Priority: <%# Eval("PriorityText") %></span>
                                                <span class="tags">Tags: <%# Eval("Tags") %></span>
                                            </div>
                                            <div class="task-description" style="margin-bottom: 10px;"><%# Eval("Description") %></div>
                                            <div class="task-footer" style="color: #27ae60; font-weight: bold;">Completed <%# DateTime.Today.ToString("MMMM dd, yyyy") %></div>
                                        </div>
                                        <asp:LinkButton ID="RemoveTaskButton" runat="server" CommandName="RemoveTask" CommandArgument='<%# Eval("TaskID") %>' CssClass="delete-task-button" style="color: #e74c3c; font-size: 1.5rem; margin-left: 15px;">&#x2715;</asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <!-- Calendar Tab -->
               <div id="calendarTab" class="tabContent" style="display:none;">
    <!-- Calendar -->
    <asp:Calendar ID="TaskCalendar" runat="server" OnDayRender="TaskCalendar_DayRender" CssClass="calendar"></asp:Calendar>

    <!-- Modal for Task Details -->
    <div id="calendarModal" class="modal" style="display:none;">
        <div class="modal-content">
            <span class="close-button" onclick="closeModal();">&times;</span>
            <h2 id="selectedDate"></h2>
            <div id="taskDetails"></div>
        </div>
    </div>
</div>

                <!-- Profile Tab -->
                <div id="profileTab" class="tabContent" style="display:none;">
                    <h2>Change Profile</h2>
                    <label for="Username">Change Username:</label>
                    <asp:TextBox ID="Username" runat="server" placeholder="Enter your new username" CssClass="input-field"></asp:TextBox><br /><br />
                    <label for="AboutYourself">What do you want people to call you?</label>
                    <textarea id="AboutYourself" runat="server" rows="5" placeholder="Tell us about yourself..." class="input-field"></textarea><br /><br />
                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                    <asp:Button ID="SubmitProfileButton" runat="server" Text="Submit" CssClass="btn-submit" OnClick="UpdateProfile_Click" />
                    <asp:Button ID="SignOutButton" runat="server" Text="Sign Out" CssClass="btn-signout" OnClick="SignOutButton_Click" />
                </div>

                <!-- Create Task Modal -->
                <div id="createTaskModal" class="modal" style="display:none;">
                    <div class="modal-content">
                        <h2>Create Task</h2>
                        <asp:TextBox ID="TaskTitle" runat="server" placeholder="Add a title" CssClass="input-field"></asp:TextBox><br />
                        <asp:TextBox ID="TaskDescription" runat="server" placeholder="Add a description" CssClass="input-field"></asp:TextBox><br />
                        <asp:DropDownList ID="TaskPriority" runat="server" CssClass="input-field">
                            <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                            <asp:ListItem Text="Medium" Value="Medium"></asp:ListItem>
                            <asp:ListItem Text="High" Value="High"></asp:ListItem>
                        </asp:DropDownList><br />
                        <asp:TextBox ID="TaskDueDate" runat="server" TextMode="Date" CssClass="input-field"></asp:TextBox>
                        <asp:TextBox ID="TaskTags" runat="server" placeholder="Add tags" CssClass="input-field"></asp:TextBox><br />
                        <asp:Button ID="CreateTaskButton" runat="server" Text="Create Task" CssClass="btn-create" OnClick="SaveTaskButton_Click" />
                        <button type="button" onclick="closeCreateTaskModal();">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script>
        function toggleFilterOptions() {
            var filterSection = document.getElementById('filterSection');
            if (filterSection.style.display === 'none' || filterSection.style.display === '') {
                filterSection.style.display = 'block'; 
            } else {
                filterSection.style.display = 'none'; 
            }
        }
        function showModal(date, details) {
            document.getElementById('selectedDate').innerText = date;
            document.getElementById('taskDetails').innerHTML = details || "No Tasks Due On This Day!";
            document.getElementById('calendarModal').style.display = 'flex';
        }

        function closeModal() {
            document.getElementById('calendarModal').style.display = 'none';
        }

        document.querySelectorAll('.calendar td').forEach(cell => {
            cell.addEventListener('click', (e) => e.stopPropagation());
        });

        window.onclick = function (event) {
            var modal = document.getElementById('calendarModal');
            if (event.target === modal) {
                closeModal();
            }
        };

        function setActiveTab(tabId) {
            document.getElementById('<%= ActiveTabField.ClientID %>').value = tabId;
     const tabs = document.querySelectorAll('.tabContent');
     tabs.forEach(tab => tab.style.display = 'none');
     document.getElementById(tabId).style.display = 'block';
 }

 function showCreateTaskModal() {
     document.getElementById('createTaskModal').style.display = 'block';
 }

 function closeCreateTaskModal() {
     document.getElementById('createTaskModal').style.display = 'none';
 }
 document.addEventListener("DOMContentLoaded", function () {
     document.querySelectorAll('.calendar td').forEach(cell => {
         cell.addEventListener('click', (e) => e.preventDefault());
     });
 });
 document.addEventListener('DOMContentLoaded', function () {
     const activeTab = document.getElementById('<%= ActiveTabField.ClientID %>').value || 'activeTab';
     setActiveTab(activeTab);
 });
    </script>
</body>

</html>
