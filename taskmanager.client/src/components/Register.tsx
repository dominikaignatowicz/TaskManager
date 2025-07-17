import React, { useEffect, useState } from "react";
import type { RegisterDto } from "../types";
import { useNavigate } from "react-router-dom";

const RegisterForm = () => {
    const [form, setForm] = useState<RegisterDto>({
        name: "",
        lastName: "",
        email: "",
        password: "",
        confirmPassword: "",
        login: ""
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setForm(perv => ({ ...perv, [name]: value }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (form.password !== form.confirmPassword) {
            alert("Has³a siê nie zgadzaj¹!");
            return;
        }

        try {
            const response = await fetch("https://localhost:7204/api/User/register", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(form)
            });

            if (!response.ok) {
                throw new Error("Rejestracja nie powiod³a siê.");
            }

            alert("Zajerestrowano pomyœlnie!");
        } catch (error) {
            alert((error as Error).message);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <input type="name" name="name" value={form.name} onChange={handleChange} placeholder="Imiê" required></input>
            <input type="lastName" name="lastName" value={form.lastName} onChange={handleChange} placeholder="Nazwisko" required></input>
            <input type="email" name="email" value={form.email} onChange={handleChange} placeholder="Email" required></input>
            <input type="login" name="login" value={form.login} onChange={handleChange} placeholder="Nazwa u¿ytkownika"></input>
            <input type="password" name="password" value={form.password} onChange={handleChange} placeholder="Has³o" required></input>
            <input type="confirmPassword" name="confirmPassword" value={form.confirmPassword} onChange={handleChange} placeholder="PotwierdŸ has³o" required></input>
            <button type="submit">Zarejestruj siê</button>
        </form>
    );
};

export default RegisterForm;