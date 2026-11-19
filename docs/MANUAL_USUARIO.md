# Manual de Usuario — Sistema Veterinario

Este manual explica cómo usar el sistema del Centro Veterinario para administrar **clientes** (los dueños) y sus **mascotas**. No necesitas conocimientos técnicos.

## 1. Inicio

Al abrir el sistema verás la pantalla de **Inicio**.

- En el **menú lateral** (a la izquierda) están las secciones: **Inicio**, **Clientes** y **Mascotas**.
- En el celular o en una pantalla pequeña el menú se oculta. Toca el icono de las **tres rayas (☰)**, arriba a la izquierda, para abrirlo.
- Las secciones marcadas como **"En preparación"** (Veterinarios, Consultas y Recetas) todavía no están disponibles.

## 2. Consultar Clientes

1. En el menú, elige **Clientes**.
2. Aparece una tabla con todos los clientes registrados.
3. En cada fila hay botones para ver el detalle, editar, eliminar y ver las mascotas del cliente.

## 3. Buscar un Cliente

1. En la pantalla **Clientes**, escribe en el cuadro de búsqueda parte del nombre. Por ejemplo: `Ana`.
2. Presiona el botón de búsqueda.
3. La tabla se reduce y muestra solo los clientes que coinciden.
4. Para volver a ver a todos, borra el texto y busca de nuevo.

## 4. Registrar un Cliente

1. En **Clientes**, presiona el botón de **nuevo cliente**.
2. Llena el formulario:
   - **Nombre** (obligatorio)
   - **Apellido paterno** (obligatorio)
   - **Teléfono** (obligatorio)
   - **Correo** (opcional; si lo escribes debe tener un formato válido, por ejemplo `nombre@correo.com`)
   - **Dirección** (opcional)
3. Presiona **Guardar**.
4. Si todo está bien, regresas al listado y aparece un mensaje verde de éxito.

**Si hay un error:** los campos con problema se marcan en rojo con un mensaje debajo, por ejemplo *"El correo no tiene un formato válido."* Corrige el dato y vuelve a guardar. El cliente no se guarda hasta que los datos estén correctos.

## 5. Editar un Cliente

1. En **Clientes**, presiona **Editar** en la fila del cliente.
2. Cambia los datos que necesites.
3. Presiona **Guardar**.
4. Aparece un mensaje de éxito y los cambios se ven en el listado.

Para salir sin cambiar nada, presiona **Cancelar**.

## 6. Ver el Detalle de un Cliente

1. En **Clientes**, presiona **Ver** en la fila del cliente.
2. Se muestran todos sus datos: nombre, teléfono, correo y dirección.
3. En esta pantalla también aparecen las mascotas del cliente.
4. Presiona **Regresar** para volver al listado.

## 7. Ver las Mascotas de un Cliente

Hay dos formas:

- Desde **Clientes**, presiona el botón **Mascotas** en la fila del cliente.
- Desde el **detalle del cliente**, presiona **Ver Mascotas**.

Se abre la lista de mascotas mostrando **solo las de ese cliente**.

## 8. Consultar y filtrar Mascotas

1. En el menú, elige **Mascotas**.
2. Aparece la tabla con **Nombre, Especie, Raza, Sexo y Propietario** de cada mascota.
3. Si la pantalla es angosta (celular), la tabla se desliza hacia los lados para ver todas las columnas y los botones de **Acciones**.
4. Para ver solo las mascotas de un cliente, usa el botón **Mascotas** desde la sección Clientes (ver punto 7).

## 9. Registrar una Mascota

1. En **Mascotas**, presiona **+ Nueva mascota**.
2. Llena el formulario:
   - **Propietario**: elige al cliente dueño de la mascota.
   - **Nombre**, **Especie**, **Raza** y **Sexo**.
   - **Fecha de nacimiento** (opcional).
   - **Color** (opcional).
3. Presiona **Guardar**.
4. Aparece un mensaje de éxito y la mascota se ve en el listado.

**Importante:** la fecha de nacimiento **no puede ser futura**. Si eliges una fecha posterior a hoy, el sistema muestra *"La fecha de nacimiento no puede ser futura."* y no guarda la mascota.

## 10. Editar una Mascota

1. En **Mascotas**, presiona **Editar** en la fila de la mascota.
2. Cambia los datos necesarios.
3. Presiona **Guardar**.
4. Verás un mensaje de éxito y los cambios en el listado.

## 11. Ver el Detalle de una Mascota

1. En **Mascotas**, presiona **Ver** en la fila de la mascota.
2. Se muestran: **Nombre, Propietario, Especie, Raza y Sexo**, y también la **fecha de nacimiento** y el **color** cuando se registraron.
3. Presiona **Regresar** para volver al listado.

## 12. Reglas de eliminación

Para proteger la información, el sistema **no permite eliminar** algunos registros.

| Registro | Se puede eliminar | No se puede eliminar |
|---|---|---|
| **Cliente** | Si **no tiene mascotas** registradas | Si tiene mascotas |
| **Mascota** | Si **no tiene consultas** registradas | Si tiene consultas |

**Cómo eliminar:**

1. Presiona **Eliminar** en la fila del registro.
2. Aparece una pantalla de confirmación con el nombre del registro.
3. Presiona **Eliminar** para confirmar, o **Cancelar** para no hacerlo.

**Si no se puede eliminar:** aparece un mensaje en rojo explicando el motivo, por ejemplo:

- *No se puede eliminar el cliente porque tiene mascotas registradas.*
- *No se puede eliminar la mascota porque tiene consultas registradas.*

En ese caso **no** aparece el botón Eliminar. Presiona **Regresar** para volver al listado.

**Para eliminar a un cliente que tiene mascotas**, primero elimina sus mascotas (si no tienen consultas) y después elimina al cliente.

## Mensajes del sistema

| Color | Significado |
|---|---|
| Verde | La operación se realizó correctamente |
| Rojo | Hubo un error o una regla impidió la acción. Lee el mensaje para saber qué corregir |
| Amarillo | Se pide una confirmación antes de continuar |

Si aparece el mensaje *"No fue posible..."*, intenta de nuevo. Si el problema continúa, avisa al responsable del sistema.
