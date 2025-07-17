import { useEffect, useState } from 'react';
import './App.css';
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import RegisterForm from "./components/Register";

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/register" element={< RegisterForm />} />
            </Routes>
        </Router>
    );
}

export default App;