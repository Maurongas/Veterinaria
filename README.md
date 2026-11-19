# Sistema Veterinario

Proyecto académico de
Ambientes de Programación Visual.

Repositorio: https://github.com/Maurongas/Veterinaria

## Tecnologías

- ASP.NET Core Razor Pages (.NET 10)
- C#
- Bootstrap
- MySQL 8
- MySql.Data (ADO.NET)
- GitHub Desktop

## Arquitectura

Web → Negocios → Datos → MySQL

Ver `docs/ARQUITECTURA.md`.

## Módulos funcionales

- Clientes: listar, buscar, crear, editar, detalle, ver mascotas y eliminar
  (sólo si no tiene mascotas).
- Mascotas: listar, filtrar por cliente, crear, editar, detalle y eliminar
  (sólo si no tiene consultas). No acepta fecha de nacimiento futura.

Veterinarios, Consultas y Recetas aparecen en el menú como "En preparación".

## Requisitos

- Visual Studio
- .NET 10 SDK
- MySQL Server 8
- MySQL Workbench

## Base de datos

1. Ejecutar `database/00_crear_bd.sql` (elimina y vuelve a crear la base local).
2. Ejecutar `database/01_datos_prueba.sql`.

Verificación final: `database/04_verificacion_final_p7.sql`.
Detalles en `database/README_BD.md`.

## Configuración local

La conexión utiliza User Secrets.
No se publican contraseñas.

En Visual Studio: clic derecho en `Veterinaria.Web` → **Manage User Secrets**:

```json
{
  "ConnectionStrings": {
    "Veterinaria": "Server=localhost;Database=Veterinaria;Uid=USUARIO;Pwd=CONTRASEÑA;"
  }
}
```

## Documentación

- `docs/LINEA_BASE_P7.md` — estado antes y después de P7
- `docs/ARQUITECTURA.md` — capas y flujos
- `docs/PRUEBAS_BACKEND.md` — revisión Backend
- `docs/PRUEBAS_FRONTEND.md` — revisión Frontend
- `docs/MATRIZ_ACEPTACION.md` — 21 casos de aceptación
- `docs/MANUAL_USUARIO.md` — uso del sistema
- `docs/PENDIENTES.md` — evolución futura

## Ramas de trabajo

- `main` — versión estable e integrada
- `backend`
- `frontend`
- `dba`

## Equipo

- Frontend: Veronica Acevedo
- Backend / Líder: Mauro Arango
- DBA / Pruebas: Josselyn Rivera
