import React, { useState, useEffect } from 'react';
import { Container } from 'react-bootstrap';
import ProfileService from '../services/ProfileService';
import 'bootstrap/dist/css/bootstrap.min.css';
import Banner from '../components/Banner'

const Profile = props => {

    const initialProfileState = {
        id: "",
        userId: "",
        profilePictureUrl: "",
        name: "",
        bio: "",
        location: "",
        website: "",
    };
    const [currentProfile, setCurrentProfile] = useState(initialProfileState);

    const getUser = id => {
        ProfileService.getProfile(id)
            .then(response => {
                setCurrentProfile(response.data);
            })
            .catch(e => {
                console.log(e);
            });
    };

    useEffect(() => {
        getUser(sessionStorage.getItem("UserId"));
      }, []);

    return (
        <Container className="pt-3">
            <Banner />
            <h3>{currentProfile.name}</h3>
        </Container>
    )
}

export default Profile;