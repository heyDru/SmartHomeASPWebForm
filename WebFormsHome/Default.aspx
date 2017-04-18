<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsHome.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SmartHome</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/bootstrap.css.map" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/site.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.js"></script>
</head>
<body>
    <div class="container">
        <form name="form1" runat="server">
            <nav class="navbar navbar-default text-center" id="navPanel" runat="server">
                <div class="container-fluid" id="addDevicePanel"  runat="server">
                </div>
                <div class="row text-center" id="consumpPanel"  runat="server">
                </div>
            </nav>
            <div class="row" id="devicePanels" runat="server">
            </div>
        </form>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#addBtn').click(function () {
                var inp = $('#consumptionBox');
                if (inp[0].value==="") {
                    alert('Please, enter consumption value');
                    return false;
                }
            });
        });
           
</script>
</body>
</html>
