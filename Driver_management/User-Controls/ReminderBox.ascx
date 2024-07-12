<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReminderBox.ascx.cs" Inherits="Driver_management.User_Controls.ReminderBox" %>


<div class="modal fade" id="reminderModal" tabindex="-1" role="dialog" aria-labelledby="reminderModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="reminderModalLabel">Reminder for Drivers</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                Remember to turn on your lights and drive slowly! <a href="#" class="alert-link">More Info</a>.
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>