﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rPrestamos.aspx.cs" Inherits="PrimerParcialAplicada2.IU.Registro.rPrestamos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <div class="form-row">
        <%--PrestamoId--%>
        <div class="form-group col-md-3">
            <asp:Label Text="Prestamo Id" class="text-primary" runat="server" />
            <asp:TextBox ID="PrestamoIdTextBox" class="form-control input-group" TextMode="Number" placeholder="0" runat="server" />

            </div>
        <%--Fecha--%>
        <div class="form-group col-md-3">
            <asp:Label Text="Fecha" runat="server" />
            <asp:TextBox ID="FechaTextBox" class="form-control input-group" TextMode="Date" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FechaTextBox" ErrorMessage="Campo Fecha obligatorio" ForeColor="Red" Display="Dynamic" SetFocusOnError="True" ToolTip="Campo Fecha obligatorio">Por favor llenar el campo Fecha </asp:RequiredFieldValidator>
        </div>

        <%--Boton--%>
        <div class="col-lg-1 p-0">
            <asp:LinkButton ID="BuscarLinkButton" CssClass="btn btn-outline-info mt-4" runat="server" OnClick="BuscarLinkButton_Click" >
                <span class="fas fa-search"></span>
                     Buscar
            </asp:LinkButton>
        </div>

    </div>

    <div class="form-row">
        <%--CuentaId--%>
        <div class="form-group col-md-3">
            <asp:Label Text="Cuenta" runat="server" />
            <asp:DropDownList ID="CuentaDropDownList"  ValidationGroup="Guardar" class="form-control input-sm" runat="server">
                <asp:ListItem Selected="True">[Seleccione]</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Guardar" runat="server" ControlToValidate="CuentaDropDownList" ErrorMessage="Campo Cuenta obligatorio" ForeColor="Red" Display="Dynamic" SetFocusOnError="True" ToolTip="Campo Cuenta obligatorio">Por favor Seleccione Cuenta </asp:RequiredFieldValidator>
        </div>

        <%--Capital--%>
        <div class="form-group col-md-3">
            <asp:Label Text="Capital" runat="server" />
            <asp:TextBox ID="CapitalTextBox" class="form-control input-group" ValidationGroup="Guardar" TextMode="Number" placeholder="0" runat="server" />
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Guardar" ControlToValidate="CapitalTextBox" ErrorMessage="Campo Capital obligatorio" ForeColor="Red" Display="Dynamic" SetFocusOnError="True" ToolTip="Campo Capital obligatorio">Por favor Llene El Campo Capital </asp:RequiredFieldValidator>
            </div>

    </div>

    <div class="form-row">
        <%--Interes--%>
        <div class="form-group col-md-3">
            <asp:Label Text="Interes" runat="server" />
            <asp:TextBox ID="InteresTextBox" class="form-control input-group" ValidationGroup="Guardar" TextMode="Number" placeholder="0" runat="server" />
         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Guardar" ControlToValidate="InteresTextBox" ErrorMessage="Campo Interes obligatorio" ForeColor="Red" Display="Dynamic" SetFocusOnError="True" ToolTip="Campo Interes obligatorio">Por favor Llene El Campo Interes </asp:RequiredFieldValidator>
        </div>

        <%--Tiempo--%>
        <div class="form-group col-md-3">
            <asp:Label Text="Tiempo" runat="server" />
            <asp:TextBox ID="TiempoTextBox" class="form-control input-group" ValidationGroup="Guardar" TextMode="Number" placeholder="0" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Guardar" ControlToValidate="TiempoTextBox" ErrorMessage="Campo Tiempo obligatorio" ForeColor="Red" Display="Dynamic" SetFocusOnError="True" ToolTip="Campo Tiempo obligatorio">Por favor Llene El Campo Tiempo </asp:RequiredFieldValidator>
        </div>

    </div>

    <div class="form-row">
        <div class="form-group ">
            <asp:Button Text="Calcular" class="btn btn-outline-primary btn-md" ID="CalcularButton" runat="server" OnClick="CalcularButton_Click"  />
        </div>
    </div>

    <%--GridDetalle--%>
    <div class="form-row justify-content-center">
        <asp:GridView ID="DetalleGridView" runat="server" class="table table-condensed table-bordered table-responsive" AutoGenerateColumns="False" CellPadding="5" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="SkyBlue" />
            <Columns>
                <asp:BoundField DataField="NoCuota" HeaderText="No Cuota" />
                <asp:BoundField DataField="Interes" HeaderText="Interes" />
                <asp:BoundField DataField="Capital" HeaderText="Capital" />
                <asp:BoundField DataField="ValorPrestamo" HeaderText="Valor Prestamo" />
                <asp:BoundField DataField="Balance" HeaderText="Balance" />
            </Columns>

        </asp:GridView>

    </div>


    <asp:Label ID="MensajeLabel" runat="server" Text=""></asp:Label>

    <%--Botones--%>
    <div class="panel-footer">
        <div class="text-center">
            <div class="form-group" style="display: inline-block">
                <asp:Button Text="Nuevo" class="btn btn-outline-info btn-md" runat="server" ID="NuevoButton" OnClick="NuevoButton_Click" />
                <asp:Button Text="Guardar" ValidationGroup="Guardar" class="btn btn-outline-success btn-md" runat="server" ID="GuadarButton" OnClick="GuadarButton_Click" />
                <asp:Button Text="Eliminar" ValidationGroup="Eliminar" class="btn btn-outline-danger btn-md" runat="server" ID="EliminarButton" OnClick="EliminarButton_Click"  />

            </div>
        </div>
    </div>

</asp:Content>
