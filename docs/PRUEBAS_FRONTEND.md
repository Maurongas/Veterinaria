# Pruebas Frontend

| No. | Prueba | Resultado |
| ---: | --- | :---: |
| 1 | Navegación | |
| 2 | Clientes | |
| 3 | Validación Cliente | |
| 4 | Formato de correo | |
| 5 | Mascotas | |
| 6 | Fecha futura | |
| 7 | Detalle Mascota | |
| 8 | Responsive 390 px | |
| 9 | Responsive 768 px | |
| 10 | Responsive escritorio | |

## Correcciones realizadas

- `Mascotas/Eliminar.cshtml`: el `@model` apuntaba a `Clientes.EliminarModel` y la página daba 404. Se corrigió a `Mascotas.EliminarModel`.
- `Mascotas/Index.cshtml`: se agregó `flex-wrap gap-2` en la fila del título para que el botón "+ Nueva mascota" no se cortara en 390 px.
- `wwwroot/css/site.css`: se agregaron `min-width: 0` y `overflow-x: auto` para que la tabla se desplace dentro de su contenedor en pantallas pequeñas.
- `wwwroot/css/site.css`: se definieron las clases `btn-florecitas` y `card-florecitas`, que se usaban en las vistas pero no tenían estilo.

## Observaciones pendientes

- La consola del navegador muestra un 404 de `veterinaria.css`, referenciado en `_Layout.cshtml`. No afecta el funcionamiento ni la apariencia.
