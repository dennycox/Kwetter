import React from 'react';
import { Container } from 'react-bootstrap';
import Banner from '../components/Banner'

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