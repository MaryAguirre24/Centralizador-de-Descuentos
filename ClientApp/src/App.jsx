import React from 'react';
import OfferFeed from './components/OfferFeed';

export default function App() {
  return (
    <div style={{ backgroundColor: '#f9fafb', minHeight: '100vh', padding: '20px 0' }}>
      <OfferFeed apiBaseUrl="http://localhost:5000/api" />
    </div>
  );
}