### [v0.1.0] - 2025-04-10

# Pulse

Una aplicación para gestionar y lanzar emuladores de videojuegos.


**Añadido:**

- Se creó un botón "Nintendo DS" en la ventana principal.
- Al hacer clic en "Nintendo DS", se abre un nuevo formulario con la lista de ROMs encontradas en la carpeta `Roms\DS`.
- Se muestra el nombre de cada ROM en la lista.
- Al seleccionar una ROM en la lista, se muestra el logo del juego (si se encuentra un archivo de imagen con el mismo nombre en la carpeta).
- Al hacer doble clic en una ROM de la lista, se intenta abrir el emulador `DeSmuME.exe` ubicado en la carpeta `Emulators`, con la ROM seleccionada como argumento.

**Modificado:**

- Se refactorizó la lógica de apertura del emulador para manejar la selección de ROMs desde la lista.

---