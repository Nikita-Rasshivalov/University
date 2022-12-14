import './App.css';
import React from 'react';
import {
  BrowserRouter,
  Routes,
  Route,
} from "react-router-dom";
import {ObjectDetection} from './components/home/Home';
import {AppRoutes} from './constants/AppRoutes';

export function App() {

  return (
    <BrowserRouter>
      <Routes>
          <Route path={AppRoutes.home} element={<ObjectDetection />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
