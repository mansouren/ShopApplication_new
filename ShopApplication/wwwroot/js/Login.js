window.addEventListener('load', function (event) {
    localStorage.clear(); //if the user refreshes the page the tokens are discarded

    
    document.getElementById('btLogin').addEventListener('click', onLoginClicked);
    document.getElementById('btLogout').addEventListener('click', onLogoutClicked);
    document.getElementById('btRevoke').addEventListener('click', onRevokeClicked);
});



async function onLoginClicked() {
    var tokens = await loginresponsess;
    saveJwtToken(tokens.token);
    saveRefreshToken(tokens.refreshToken);
    fetchWithCredentials('/Admin/');
}



function onLogoutClicked() { //wouldn't be a bad idea to send a request to the server that would invalidate the refresh token
    localStorage.clear();
    
}

function getJwtToken() {
    return localStorage.getItem('token');
}
function saveJwtToken(token) {
    localStorage.setItem('token', token);
}

function saveRefreshToken(refreshToken) {
    localStorage.setItem('refreshToken', refreshToken);
}

function getRefreshToken() {
    return localStorage.getItem('refreshToken');
}


async function onRevokeClicked() {
    writeFeedback('');
    var revokeResponse = await revoke();
    if (revokeResponse.ok) {
        writeFeedback('Refresh token was revoked, when the access token (JWT) expires authenticated requests will start to fail');
    } else {
        writeFeedback(`Revoke failed with status code: ${revokeResponse.status}`);
    }
}

async function fetchWithCredentials(url, options) {
    var jwtToken = getJwtToken();
    options = options || {};
    options.headers = options.headers || {};
    options.headers['Authorization'] = 'Bearer ' + jwtToken;
    var response = await fetch(url, options);
    if (response.ok) { //all is good, return the response
        return response;
    }

    if (response.status === 401 && response.headers.has('Token-Expired')) {
        var refreshToken = getRefreshToken();

        var refreshResponse = await refresh(jwtToken, refreshToken);
        if (!refreshResponse.ok) {
            return response; //failed to refresh so return original 401 response
        }
        var jsonRefreshResponse = await refreshResponse.json(); //read the json with the new tokens

        saveJwtToken(jsonRefreshResponse.token);
        saveRefreshToken(jsonRefreshResponse.refreshToken);
        return await fetchWithCredentials(url, options); //repeat the original request
    } else { //status is not 401 and/or there's no Token-Expired header
        return response; //return the original 401 response
    }
}

function refresh(jwtToken, refreshToken) {
    return fetch('token/refresh', {
        method: 'POST',
        body: `token=${encodeURIComponent(jwtToken)}&refreshToken=${encodeURIComponent(getRefreshToken())}`,
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        }
    });
}

function revoke() {
    return fetchWithCredentials('token/revoke', {
        method: 'POST'
    });
}