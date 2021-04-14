(async function () {
    const createInfoRow = function createInfoRow(infoRow) {
        const generateHeaderText = (title) => {
            return title[0].toUpperCase() + title.slice(1, title.length);
        }
        const generateHeaderIcon = (title) => {
            const headerIcons = {
                birthday: "fas fa-birthday-cake",
                location: "fas fa-map-marker-alt"
            };

            if(headerIcons.hasOwnProperty(title)) {         
                return `<i class="${headerIcons[title]}"></i>`   
            }
            return '';
        }

        const headerText = generateHeaderText(infoRow.title);
        const headerIcon = generateHeaderIcon(infoRow.title);
        const content = `<div>${infoRow.content}</div>`;
        return `
            <div>
                <b>
                    ${headerIcon}
                    ${headerText}
                </b>
            </div>
            ${content}
        `;
    };

    const getUserInfo = async function getUserInfo(id) {
        const url = `https://localhost:5001/Account/GetUserBoxInfo/${id}`;
        const response = await fetch(url);
        const data = await response.json();
        return data;
    }

    const populateInfoBox = function populateInfoBox(data) {
        const isValidProperty = function isValidProperty(property) {
            const validProperties = [
                "fullName",
                "age",
                "birthday",
                "joinedOn",
                "location"
            ]
            const index = validProperties.indexOf(property);
            return index >= 0;
        }

        const userInfoDetails = document.getElementById("user-info-details");

        const userInfoHTML = Object.keys(data).reduce((acc, key) => {
            if(data[key] && isValidProperty(key)) {
                acc += createInfoRow({
                    title: key,
                    content: data[key]
                });
            }
            return acc;
        }, "");
    
        userInfoDetails.innerHTML = userInfoHTML;

        const usernameElement = document.getElementById("username");
        usernameElement.textContent = data.username;

        const profilePictureElement = document.getElementById("profilePicture");
        const imageElement = document.createElement("img");
        imageElement.setAttribute("src", data.profilePictureUrl);
        imageElement.className = "profilePicture";
        profilePictureElement.appendChild(imageElement)
    }

    window.addEventListener('load', async (e) => {
        const currentUserId = userId; // lets say its a global variable for now
        const userInfo = await getUserInfo(currentUserId);
        populateInfoBox(userInfo);
    });
}())