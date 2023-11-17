import React from "react";
import ReactDOM from "react-dom";

import App from "./App.tsx";

import { BrowserRouter, Route, Routes } from "react-router-dom";
import Test from "./test.tsx";

ReactDOM.render(
    <React.StrictMode>
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<App />} />
                <Route path="/test" element={<Test />} />
            </Routes>
        </BrowserRouter>
    </React.StrictMode>,
    document.getElementById("root")
)

