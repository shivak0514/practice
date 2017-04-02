<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeDetails.aspx.cs" Inherits="DemoApplication.EmployeeDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://code.jquery.com/jquery-3.1.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#grdEmp_txtWork_0").on('mouseover', function () {
                $(this).attr('title', $("#grdEmp_lblDescription_0").text())
            });
            $("#grdEmp_txtWork_1").on('mouseover', function () {
                $(this).attr('title', $("#grdEmp_lblDescription_1").text())
            });
            $("#grdEmp_txtWork_2").on('mouseover', function () {
                $(this).attr('title', $("#grdEmp_lblDescription_2").text())
            });
        });

        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {

                        inputList[i].checked = true;
                    }
                    else {

                        inputList[i].checked = false;
                    }
                }
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="grdEmp" class="table table-hover table-nomargin table-bordered CustomerAddress"
                Style="margin-bottom: 0.83%; margin-top: 0.83%; width: 50%;" runat="server" ShowFooter="true"
                AutoGenerateColumns="False" OnRowDataBound="grdEmp_DataBound">
                <AlternatingRowStyle BackColor="#EEEEEE" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#ffffff" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <HeaderTemplate>
                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chbDept" runat="server" Text="" />
                            <%-- onclick="CheckSingleShiftCheckbox(this)"
                                OnCheckedChanged="chbDept_CheckedChanged" AutoPostBack="true" CommandName='<%#Eval("Dept_Code")%>' --%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:TextBox ID="txtName" runat="server" class="form-control" Text='<%#Eval("Name")%>'></asp:TextBox>
                            <asp:Label ID="lblId" runat="server" class="control-label" Visible="false" Text='<%#Eval("Id")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Work">
                        <ItemTemplate>
                            <asp:TextBox ID="txtWork" runat="server" class="form-control" Text='<%#Eval("Work")%>'></asp:TextBox>
                            <asp:Label ID="lblDescription" runat="server" class="control-label" Style="visibility: hidden;" Text='<%#Eval("Description")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="status">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlStatus" runat="server" class="form-control DropdownCss" DataTextField="Status" DataValueField="Status">
                                <asp:ListItem Text="--Select--" Value="Select"></asp:ListItem>
                                <asp:ListItem Text="Complete" Value="Complete"></asp:ListItem>
                                <asp:ListItem Text="InComplete" Value="InComplete"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblstatus" runat="server" class="control-label" Visible="false" Text='<%#Eval("Status")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <HeaderTemplate>
                            <asp:Button ID="btnUpdateAll" runat="server" Text="Update All" OnClick="btnUpdateAll_Click" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                            <asp:LinkButton ID="lbRemove" runat="server" Text="Remove" OnClick="lbRemove_Click">
                            </asp:LinkButton> 
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:LinkButton ID="btnAddNewRecord" runat="server" Text="Add" OnClick="btnAddNewRecord_Click">
                            </asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
