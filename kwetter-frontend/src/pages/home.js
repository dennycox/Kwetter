import React from 'react';
import { Container, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';

function Home() {
    return (
        <div>
            <Banner />
            <Container className="pt-3">
                <h2>Home</h2>
            </Container>
        </div>
    )
}

export default Home;