# Línea base — Práctica 7

Fecha: 18/11/2026
Equipo: Florecitas

## Equipo

- Frontend: Veronica Acevedo
- Backend / Líder: Mauro Arango
- DBA / Pruebas: Josselyn Rivera

## Estado inicial

- Commit de main: `e1c49cf` (Merge pull request #11 from Maurongas/frontend)
- Compila: Sí (Build succeeded, 0 errores, 0 advertencias)
- Ejecuta: Sí
- MySQL: Sí (base `Veterinaria`, 9 tablas)
- Clientes: Sí
- Mascotas: Sí

## Problemas conocidos antes de corregir

- `Clientes/Eliminar` y `Mascotas/Eliminar`: en `OnPost` la carga del registro
  se hacía antes del `try`; una falla de MySQL terminaba en página 500.
- `Program.cs` registraba `Conexion`, `ClienteDatos` y `MascotaDatos` como
  servicios e importaba `Veterinaria.Datos`, por lo que la capa Web hacía
  referencia directa a la capa de Datos. Ninguna página utilizaba esos registros.
- `Mascotas/Nueva` y `Mascotas/Editar`: el error de carga de clientes se enviaba
  a `ModelState` en lugar de la propiedad `Error`.
- La base local conservaba registros temporales de P6 (`prueba p6`, `pixel 6`).

## Estado final

- Compila: Sí (Build succeeded, 0 errores)
- Ejecuta: Sí (F5 y Ctrl + F5)
- MySQL: Sí (9 tablas, 9 PK, 8 FK, UPDATE CASCADE / DELETE RESTRICT)
- Clientes: APROBADO
- Mascotas: APROBADO
- Matriz de aceptación: ver `docs/MATRIZ_ACEPTACION.md` (completada por DBA)

## Problemas resueltos

- Páginas Eliminar: la carga del registro en `OnPost` quedó dentro del `try` (rama `frontend`).
- `Program.cs`: Web ya no registra ni importa clases de `Veterinaria.Datos` (rama `backend`).
- `Mascotas/Nueva` y `Mascotas/Editar`: el error de carga de clientes usa la propiedad `Error` (rama `frontend`).
- `Mascotas/Detalle`: ahora muestra fecha de nacimiento y color (rama `frontend`).
- Base local: reconstruida con `00_crear_bd.sql` + `01_datos_prueba.sql` antes de la aceptación.

## Problemas pendientes

- Ninguno dentro del alcance obligatorio.
