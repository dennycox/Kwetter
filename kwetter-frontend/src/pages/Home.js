import React from 'react';
import { Container } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import Banner from '../components/Banner'

const Home = () => {

    return (
        <Container className="pt-3">
            <Banner />
            <h3>Home</h3>
            <p>No tweets yet</p>
        </Container>
    )
}

export default Home;