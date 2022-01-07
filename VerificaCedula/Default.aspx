<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VerificaCedula.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>

    <form id="form1" runat="server">
        <div>
           <h3> Verificar Datos </h3>
            Número : <asp:TextBox ID="numero" runat="server" ClientIDMode="Static" Text="99999999"></asp:TextBox>
            <br />
            Nombre archivo :<asp:TextBox ID="nombrearchivo" runat="server"  ClientIDMode="Static" Text="imagen.jpg"></asp:TextBox>
            
            <br /><br />
            <div id="imagen"></div> 
            <br />
            
            <div id="verificando" > 
               Verificando ....      
            </div>
            
            <div id="true" style="display:none"> 
                El dato es correcto puede continuar 
                <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" />
            </div>
            <div id="false" style="display:none">
                El dato es incorrecto verifique los datos  
                <asp:Button ID="btnAtras" runat="server" Text="Atras" />
            </div>
            
        </div>



        <script type="text/javascript">
            $(function () {

                var nombrearchivo = $("#nombrearchivo").val();
                var numero = $("#numero").val();

                var div = document.getElementById('imagen');
                var img1 = new Image();
               
                img1.src = "imagenes/" + nombrearchivo;
                img1.width = 250;
                div.appendChild(img1);
               // alert(img1.src)

               // alert(nombrearchivo)
                $.ajax({
                    type: "POST",
                    url: "Default.aspx/Verificar",
                    data: "{NombreArchivo: '" + nombrearchivo + "',numero:'" + numero + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                       // alert(r.d);
                        $("#verificando").hide();

                        if (r.d = "true") {
                            $("#true").show();
                        } else {
                            $("#false").show();
                        }


                    }
                }); 
            });

  
        </script>



    </form>
</body>
</html>
