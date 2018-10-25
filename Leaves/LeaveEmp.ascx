<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeaveEmp.ascx.cs" Inherits="SMEPayroll.Leaves.LeaveEmp" %>
<div style="width:395px;background-color:Transparent ;font-family:Tahoma ;"><table style="background-color=white">
    <tr>
        <td rowspan="7" style="width: 100px">
       <img src="../Frames/Images/EMPLOYEE/employee.png" alt="Employee Image" />
        </td>
        <td style="width: 295px">
        Name : <%=Name%> 
        </td>
    </tr>
    <tr>
        <td style="width: 295px">
        Period :<%=startDate%> to <%=endDate%>
        </td>
    </tr>
    <tr>
        <td style="width: 295px">
        Leave Type : <%=leaveType %>
        </td>
    </tr>
    <tr>
        <td style="width: 295px">
        No Of Days : <%=noa %>
        </td>
    </tr>
    <tr>
        <td style="width: 295px">
        Approver : <%=approver %>
        </td>
    </tr>
    <tr>
        <td style="width: 295px">
        Department : <%=dept %>
        </td>
    </tr>
    <tr>
        <td style="width: 295px">
        Contact : <%=contact %>
        </td>
    </tr>
</table>
</div>
