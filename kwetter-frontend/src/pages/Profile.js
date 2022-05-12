import React, { useState, useEffect } from 'react';
import { Container } from 'react-bootstrap';
import ProfileService from '../services/ProfileService';
import 'bootstrap/dist/css/bootstrap.min.css';
import Banner from '../components/Banner'

const Profile = props => {

    const initialProfileState = {
        id: "",
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
            <h4><b>Name</b></h4>
            <h4>{currentProfile.name}</h4>
            <h4><b>Bio</b></h4>
            <h4>{currentProfile.bio}</h4>
            <h4><b>Location</b></h4>
            <h4>{currentProfile.location}</h4>
            <h4><b>Website</b></h4>
            <h4>{currentProfile.website}</h4>
        </Container>
    )
}

export default Profile;