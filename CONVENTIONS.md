# Convenciones del Proyecto - Centralizador de Descuentos

Este documento define las reglas técnicas, de código y de interfaz mínimas y pragmáticas para el desarrollo de nuevas funcionalidades en el MVP del centralizador de descuentos de comida rápida.

---

## 1. Convenciones de Código y Estructura (ASP.NET Core)

* **Estructura Modular:** Organizar el código por dominios de negocio o funcionalidades (ej. `CatalogManagement`, `DiscoveryAndFeed`, `Redemption`, `Analytics`) en lugar de capas técnicas masivas.
* **Estándar de Nomenclatura C#:**
  * `PascalCase` para clases, métodos y propiedades (`Offer`, `GetActiveOffers`).
  * `camelCase` para variables locales y parámetros.
* **Persistencia y Base de Datos:**
  * Uso de **SQLite con Entity Framework Core**.
  * En desarrollo, se permite el uso de migraciones automáticas o recreación rápida de la base de datos local si el esquema cambia, priorizando la velocidad sobre la complejidad de migraciones avanzadas.

---

## 2. Convenciones de Interfaz de Usuario (UI/UX)

* **Estilos:** Utilizar **Bootstrap o Tailwind CSS** mediante CDN directo en la vista base (`_Layout.cshtml`), evitando configuraciones complejas con empaquetadores de nodos externos (Node.js/Vite/Webpack) en esta fase de MVP.
* **Tarjetas y Restricciones:**
  * Máximo de 3 viñetas de restricciones clave por tarjeta de oferta.
  * Uso de etiquetas (badges) estandarizadas para indicar monto mínimo y estado de vencimiento.
* **Interacción (Copiado de Cupón):**
  * Uso de la API nativa del navegador `navigator.clipboard.writeText()`.
  * Feedback visual directo en el botón (cambio temporal de texto, ej. de "Copiar" a "¡Copiado!") en lugar de componentes flotantes complejos.