import React from 'react';
import { Container, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';

function Start() {
    return (
        <div>
            <Container className="pt-3">
                <h2>Kwetter</h2>
                <h3>What's happening</h3>
                <Link to={"/register"}>
                    <Button variant="primary">Register</Button>
                </Link>
                <Link to={"/login"}>
                    <Button variant="primary">Login</Button>
                </Link>
            </Container>
        </div>
    )
}

export default Start;