# API SGPA V2 Inventory

API SGPA V2 Inventory es una API RESTful para gestionar un inventario. Esta API permite crear, leer, actualizar y eliminar productos como su respectiva categoría.

## Características

- Gestión completa de productos (CRUD).
- Implementado con .NET 8.0 y Entity Framework Core.
- Base de datos SQL Server.

## Instalación

1. Clona este repositorio.
2. Abre la solución en Visual Studio.
3. Asegúrate de tener instalado .NET 8.0 y SQL Server.
4. Actualiza la cadena de conexión en `appsettings.json` con tus propios detalles de SQL Server.
5. Ejecuta las migraciones para crear la base de datos con `Update-Database` en la consola del administrador de paquetes.
6. Ejecuta la aplicación.

## Uso

Puedes usar herramientas como [Postman](https://www.postman.com/) o [curl](https://curl.se/) para hacer solicitudes a la API.

## Licencia

[MIT](https://choosealicense.com/licenses/mit/)
