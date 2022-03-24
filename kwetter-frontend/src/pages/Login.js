import React, { useState } from "react";
import { Button, Container, Form } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import AuthenticationService from "../services/AuthenticationService";
import 'bootstrap/dist/css/bootstrap.min.css';

function Login() {
    const [login, setLogin] = useState({ email: "", password: "" });
    const [message, setMessage] = useState("");

  const handleChange = (event) => {
    setLogin({ ...login, [event.target.name]: event.target.value });
  };

  const handleSubmit = (event) => {
    event.preventDefault();

    AuthenticationService.login(login)
      .then((res) => {
        localStorage.setItem("Token", res.data.token);
        sessionStorage.setItem("UserId", res.data.id);
        setMessage("Login successful");
      })
      .catch((error) => {
        setMessage("Account information does not match");
      });
  };

    return (
        <Container className="pt-3">
            <h3>Login</h3>
            <Form onSubmit={handleSubmit}>
                <Form.Group>
                    <Form.Label>E-mail address</Form.Label>
                    <Form.Control type="email" onChange={handleChange} name="email" required />
                </Form.Group>
                <Form.Group>
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" onChange={handleChange} name="password" required />
                </Form.Group>
                <Form.Group>
                    <Button variant="primary" type="submit" >Login</Button>
                </Form.Group>
                <Link to={"/register"}>Don't have an account? Create an account here</Link>
            </Form>
            <div
                className="text-danger mx-auto"
                role="alert"
                style={{ textAlign: "center" }}
              >
                {message}
              </div>
        </Container>
    )
}

export default Login;