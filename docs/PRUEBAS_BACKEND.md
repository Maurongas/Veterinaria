# Pruebas Backend

Responsable: Mauro Arango (Backend / Líder)
Rama: `backend`

## Arquitectura

- Web sin SQL: **Sí**. Búsqueda en `Veterinaria.Web/**/*.cs` de `SELECT`,
  `INSERT INTO`, `UPDATE`, `DELETE FROM`: sin coincidencias de SQL.
- Web sin MySqlConnection: **Sí**. Sin coincidencias de `MySqlConnection`,
  `MySqlCommand` ni `MySqlDataReader` en `Veterinaria.Web`.
- Web sin referencia directa a Datos: **Sí** (después de la corrección de `Program.cs`).
- Negocios usa Datos: **Sí**. `ClienteNegocio` → `ClienteDatos`, `MascotaNegocio` → `MascotaDatos`.
- Datos usa MySql.Data: **Sí**. `ClienteDatos`, `MascotaDatos` y `Conexion`.
- Entidades no depende de otras capas: **Sí**. `Veterinaria.Entidades.csproj` no tiene referencias de proyecto.
- Negocios no contiene HTML: **Sí**.

## Reglas

- Cliente con Mascotas: `ClienteNegocio.Eliminar()` consulta `TieneMascotas()` y lanza
  `ReglaNegocioException("No se puede eliminar el cliente porque tiene mascotas registradas.")`. **Aprobado**.
- Mascota con Consultas: `MascotaNegocio.Eliminar()` consulta `TieneConsultas()` y lanza
  `ReglaNegocioException("No se puede eliminar la mascota porque tiene consultas registradas.")`. **Aprobado**.
- Fecha futura: `MascotaNegocio.Validar()` rechaza `FechaNacimiento > DateTime.Today`
  con el mensaje "La fecha de nacimiento no puede ser futura.". **Aprobado**.
- `ReglaNegocioException` existe en `Veterinaria.Negocios/ReglaNegocioException.cs`. **Aprobado**.
- Eliminar valida Id (`<= 0` → `ArgumentException`). **Aprobado**.

## Consultas parametrizadas

- ClienteDatos: `Listar`, `Buscar` (`@termino`), `BuscarPorId` (`@idCliente`),
  `Agregar`, `Modificar`, `TieneMascotas`, `Eliminar`. Todas usan
  `Parameters.AddWithValue`; no hay concatenación de datos del usuario. **Aprobado**.
- MascotaDatos: `Listar`, `ListarPorCliente` (`@idCliente`), `BuscarPorId` (`@idMascota`),
  `Agregar`, `Modificar`, `TieneConsultas`, `Eliminar`. Todas parametrizadas. **Aprobado**.

## Manejo de errores

- Páginas Eliminar: `ReglaNegocioException` → `Error = ex.Message`;
  `Exception` → `Debug.WriteLine(ex)` + mensaje amigable. **Aprobado**.
- La carga del registro en `OnPost` de las páginas Eliminar quedó dentro del `try`
  (corrección integrada desde la rama `frontend`). **Aprobado**.
- No se muestra `ex.ToString()`, stack trace ni cadena de conexión al usuario. **Aprobado**.

## Program.cs

- Lee `ConnectionStrings:Veterinaria` desde User Secrets. **Sí**.
- Inicializa Negocios con `ConfiguracionNegocio.Inicializar(cadena)`. **Sí**.
- No contiene contraseña escrita. **Sí**.
- Arranque no bloqueante si falta la cadena (sólo `Debug.WriteLine`). **Sí**.
- Un solo `WebApplication.CreateBuilder` y un solo `AddRazorPages`. **Sí**.

## Compilación

- `Build Solution` (Debug): **Build succeeded, 0 errores**.

## Correcciones realizadas

- `Veterinaria.Web/Program.cs`: se eliminaron los registros
  `AddScoped<Conexion>`, `AddScoped<ClienteDatos>`, `AddScoped<MascotaDatos>`,
  `AddScoped<ClienteNegocio>`, `AddScoped<MascotaNegocio>` y el
  `using Veterinaria.Datos;`. Las páginas crean los objetos de Negocios con `new()`,
  por lo que esos registros no se usaban y hacían que Web dependiera directamente
  de la capa de Datos.
