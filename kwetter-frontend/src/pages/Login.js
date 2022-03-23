import React from 'react';
import { Button, Container, Form } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';

function Login() {
    return (
        <Container className="pt-3">
            <h3>Login</h3>
            <Form>
                <Form.Group>
                    <Form.Label>E-mail address</Form.Label>
                    <Form.Control type="email" />
                </Form.Group>
                <Form.Group>
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" />
                </Form.Group>
                <Form.Group>
                    <Button variant="primary">Login</Button>
                </Form.Group>
                <Link to={"/register"}>Don't have an account? Create an account here</Link>
            </Form>
        </Container>
    )
}

export default Login;