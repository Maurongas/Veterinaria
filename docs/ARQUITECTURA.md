# Arquitectura

El sistema usa una arquitectura por capas. Cada capa sólo conoce a la capa
inmediatamente inferior.

## Veterinaria.Web

Interfaz y Razor Pages (Bootstrap).

- Páginas `Clientes/*` y `Mascotas/*`.
- Crea objetos de Negocios (`ClienteNegocio`, `MascotaNegocio`).
- No contiene SQL ni `MySqlConnection`.
- `Program.cs` lee `ConnectionStrings:Veterinaria` desde User Secrets y llama a
  `ConfiguracionNegocio.Inicializar(cadena)`.

## Veterinaria.Negocios

Reglas y validaciones.

- `ClienteNegocio`: valida datos obligatorios; impide eliminar un cliente con mascotas.
- `MascotaNegocio`: valida propietario, nombre, especie y fecha no futura;
  impide eliminar una mascota con consultas.
- `ReglaNegocioException`: excepción para reglas funcionales cuyo mensaje sí
  puede mostrarse al usuario.
- No contiene HTML.

## Veterinaria.Datos

ADO.NET y MySQL (`MySql.Data`).

- `Conexion`: crea `MySqlConnection` con la cadena recibida desde Negocios.
- `ClienteDatos`, `MascotaDatos`: SELECT / INSERT / UPDATE / DELETE con
  consultas parametrizadas (`@idCliente`, `@idMascota`, `@termino`, ...).

## Veterinaria.Entidades

Modelos compartidos (`Cliente`, `Mascota`) con Data Annotations.
No depende de ninguna otra capa.

## Flujo

```text
Web
↓
Negocios
↓
Datos
↓
MySQL
```

## Ejemplo: eliminar un Cliente

```text
Clientes/Eliminar.cshtml.cs  (OnPost)
        ↓
ClienteNegocio.Eliminar(id)
        ↓  ClienteDatos.TieneMascotas(id)
        ├── true  → throw ReglaNegocioException
        │           → la página muestra Model.Error y oculta el botón Eliminar
        └── false → ClienteDatos.Eliminar(id)
                    → DELETE FROM cliente WHERE id_cliente = @idCliente
                    → TempData["Mensaje"] y redirección a Index
```

## Ejemplo: registrar una Mascota

```text
Mascotas/Nueva.cshtml.cs  (OnPost)
        ↓  ModelState (Data Annotations)
MascotaNegocio.Agregar(mascota)
        ↓  Validar(): propietario > 0, nombre, especie, fecha no futura
MascotaDatos.Agregar(mascota)
        ↓
INSERT INTO mascota (...) VALUES (@idCliente, @nombre, ...)
```

## Manejo de errores

| Tipo | Tratamiento |
| --- | --- |
| Formulario Nuevo / Editar | Data Annotations + `ModelState`; se conservan los datos capturados |
| Página de consulta (Index / Detalle) | Propiedad `Error` visible en la vista |
| Regla de negocio (Eliminar) | `ReglaNegocioException` → `Error = ex.Message` |
| Error técnico | `Debug.WriteLine(ex)` + mensaje amigable; nunca se muestra el detalle |

La integridad también se protege en MySQL: las llaves foráneas usan
`ON DELETE RESTRICT` y `ON UPDATE CASCADE`.
