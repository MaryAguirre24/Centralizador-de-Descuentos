import React, { useState, useEffect } from 'react';
import React from 'react';
import OfferFeed from './components/OfferFeed';

export default function App() {
  return (
    <main style={{ backgroundColor: '#f9fafb', minHeight: '100vh', padding: '20px 0' }}>
      <OfferFeed apiBaseUrl="http://localhost:5000/api" />
    </main>
  );
}
export default function OfferFeed({ apiBaseUrl = 'http://localhost:5000/api' }) {
  const [offers, setOffers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [copiedId, setCopiedId] = useState(null);
  const [error, setError] = useState(null);

  // Cargar ofertas reales desde el backend
  useEffect(() => {
    fetch(`${apiBaseUrl}/feed`)
      .then((res) => {
        if (!res.ok) throw new Error('Error al conectar con la base de datos');
        return res.json();
      })
      .then((data) => {
        setOffers(data);
        setLoading(false);
      })
      .catch((err) => {
        console.error(err);
        setError('No se pudieron cargar las ofertas.');
        setLoading(false);
      });
  }, [apiBaseUrl]);

  // Registro de clics de salida (Analítica)
  const trackClick = (offerId, actionType) => {
    fetch(`${apiBaseUrl}/analytics/click`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ offerId, actionType }),
    }).catch((err) => console.error('Error registrando métrica:', err));
  };

  // Copiar código en 1 clic con feedback visual instantáneo
  const handleCopyCode = (offerId, code) => {
    navigator.clipboard.writeText(code);
    setCopiedId(offerId);
    trackClick(offerId, 'COPY_CODE');

    // Restablecer el feedback visual tras 2 segundos
    setTimeout(() => {
      setCopiedId(null);
    }, 2000);
  };

  const handleRedirect = (offerId, url) => {
    trackClick(offerId, 'REDIRECT');
    window.open(url, '_blank', 'noopener,noreferrer');
  };

  if (loading) {
    return <div style={{ textAlign: 'center', padding: '40px', color: '#6b7280' }}>Cargando mejores ofertas...</div>;
  }

  if (error) {
    return <div style={{ textAlign: 'center', padding: '40px', color: '#dc2626' }}>{error}</div>;
  }

  return (
    <div style={{ maxWidth: '650px', margin: '0 auto', padding: '16px', fontFamily: 'system-ui, -apple-system, sans-serif' }}>
      <header style={{ textAlign: 'center', marginBottom: '24px' }}>
        <h1 style={{ color: '#d97706', fontSize: '1.8rem', margin: '0 0 8px 0' }}>🍔 Centralizador de Descuentos</h1>
        <p style={{ color: '#4b5563', margin: 0, fontSize: '0.95rem' }}>
          Promociones destacadas de comida rápida listas en menos de 3 segundos
        </p>
      </header>

      <div style={{ display: 'flex', flexDirection: 'column', gap: '16px' }}>
        {offers.map((offer) => {
          const hasCode = Boolean(offer.promoCode && offer.promoCode.trim() !== '');

          return (
            <article
              key={offer.id}
              style={{
                border: '1px solid #e5e7eb',
                borderRadius: '12px',
                padding: '18px',
                backgroundColor: '#ffffff',
                boxShadow: '0 2px 8px rgba(0,0,0,0.05)',
              }}
            >
              {/* Encabezado con Nombre del Comercio e Indicadores */}
              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '10px' }}>
                <span style={{ fontWeight: '700', color: '#111827', fontSize: '1.1rem' }}>
                  {offer.storeName}
                </span>

                <div style={{ display: 'flex', gap: '6px' }}>
                  {/* Badge: Monto Mínimo */}
                  <span
                    style={{
                      backgroundColor: '#fef3c7',
                      color: '#92400e',
                      padding: '4px 8px',
                      borderRadius: '6px',
                      fontSize: '0.75rem',
                      fontWeight: '600',
                    }}
                  >
                    {offer.minimumSpend || 'Sin mínimo'}
                  </span>

                  {/* Badge: Expiración */}
                  <span
                    style={{
                      backgroundColor: '#fee2e2',
                      color: '#991b1b',
                      padding: '4px 8px',
                      borderRadius: '6px',
                      fontSize: '0.75rem',
                      fontWeight: '600',
                    }}
                  >
                    {offer.expirationDaysLeft === 0
                      ? 'Vence hoy'
                      : `Vence en ${offer.expirationDaysLeft} días`}
                  </span>
                </div>
              </div>

              {/* Título de la promoción */}
              <h2 style={{ margin: '0 0 12px 0', color: '#1f2937', fontSize: '1.25rem' }}>
                {offer.title}
              </h2>

              {/* Viñetas de restricciones clave (Máximo 3) */}
              <ul style={{ paddingLeft: '20px', margin: '0 0 16px 0', color: '#4b5563', fontSize: '0.9rem', lineHeight: '1.4' }}>
                {offer.restrictions &&
                  offer.restrictions.slice(0, 3).map((restriction, index) => (
                    <li key={index} style={{ marginBottom: '4px' }}>
                      {restriction}
                    </li>
                  ))}
              </ul>

              {/* Botones de Acción e Interacción */}
              <div style={{ display: 'flex', gap: '10px' }}>
                {hasCode ? (
                  <button
                    onClick={() => handleCopyCode(offer.id, offer.promoCode)}
                    style={{
                      flex: 1,
                      padding: '10px 14px',
                      backgroundColor: copiedId === offer.id ? '#16a34a' : '#2563eb',
                      color: '#ffffff',
                      border: 'none',
                      borderRadius: '8px',
                      fontWeight: '600',
                      cursor: 'pointer',
                      fontSize: '0.9rem',
                      transition: 'background-color 0.2s ease',
                    }}
                  >
                    {copiedId === offer.id ? '¡Código copiado!' : `Copiar código: ${offer.promoCode}`}
                  </button>
                ) : (
                  <button
                    onClick={() => handleRedirect(offer.id, offer.originalUrl)}
                    style={{
                      flex: 1,
                      padding: '10px 14px',
                      backgroundColor: '#2563eb',
                      color: '#ffffff',
                      border: 'none',
                      borderRadius: '8px',
                      fontWeight: '600',
                      cursor: 'pointer',
                      fontSize: '0.9rem',
                    }}
                  >
                    Ir a la oferta ↗
                  </button>
                )}

                {hasCode && (
                  <button
                    onClick={() => handleRedirect(offer.id, offer.originalUrl)}
                    style={{
                      padding: '10px 14px',
                      backgroundColor: '#f3f4f6',
                      color: '#374151',
                      border: '1px solid #d1d5db',
                      borderRadius: '8px',
                      fontWeight: '500',
                      cursor: 'pointer',
                      fontSize: '0.9rem',
                    }}
                  >
                    Ir a la oferta ↗
                  </button>
                )}
              </div>
            </article>
          );
        })}
      </div>
    </div>
  );
}