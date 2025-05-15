window.addEventListener("load", () => {
    document.getElementById('refresh').addEventListener('click',getServers);
    document.getElementById('toggle-empty').addEventListener('click', () => {
        showEmptyServers = !showEmptyServers;
        document.getElementById('toggle-empty').textContent = showEmptyServers
          ? 'Hide Empty Servers'
          : 'Show Empty Servers';
        getServers();
    });
  getServers();

  // Auto-refresh every 60 seconds
  setInterval(() => {
    getServers();
  }, 60000);
});

let showEmptyServers = false;

async function getServers() {
    const endpoint = "http://localhost:5252/api/BattleBit/servers";
    const serverListDiv = document.getElementById('servers');
    const statusDiv = document.getElementById('status');

    try {
        statusDiv.textContent = 'Loading...'
        document.getElementById('loading-overlay').classList.add('visible');
        requestAnimationFrame(() => {
            serverListDiv.innerHTML = '';
          });

        const res = await fetch(endpoint);
        if(!res.ok) {
            throw new Error(`Response status: ${res.status}`);
        }

        const now = new Date().toLocaleTimeString();
        statusDiv.textContent = `Last updated at ${now}`;
        document.getElementById('loading-overlay').classList.remove('visible');

        const servers = await res.json();

        // Debug Server Response
        // console.log(servers);

        const filtered = showEmptyServers ? servers : servers.filter(s => s.players > 0);
        const sorted = filtered.sort((a,b) => b.players - a.players);
        const grouped = new Map();

        sorted.forEach(server => {
            const region = server.region;

            if(!grouped.has(region)) {
                grouped.set(region, []);
            }

            grouped.get(region).push(server);
        });

        grouped.forEach((servers, region) => {
            if (servers.length === 0) return;
            const regionDiv = document.createElement('div');
            const regionName = region.split('_')[0];
            const formattedRegion = regionName.charAt(0).toUpperCase() + regionName.slice(1);
            const totalPlayers = servers.reduce((sum,server) => sum + server.players, 0);
            regionDiv.className = 'region-group';
            regionDiv.innerHTML = `
                <h2 class="region-header">
                    <span class="toggle-icon">▼</span> 
                    <span class="region-name">${formattedRegion}</span>
                    <span class="player-count">👥 <strong>${totalPlayers.toLocaleString()}</strong></span>
                </h2>
                <div class="region-servers"></div>
                `;

            const header = regionDiv.querySelector('.region-header');
            const serversContainer = regionDiv.querySelector('.region-servers');
            const toggleIcon = regionDiv.querySelector('.toggle-icon');
            
            header.addEventListener('click', () => {
                serversContainer.classList.toggle('hidden');
                toggleIcon.classList.toggle('collapsed');
            });

            servers.forEach(server => {
                const isFull = (server.players + server.queuePlayers) >= server.maxPlayers;
                const isActive = (server.players + server.queuePlayers) / server.maxPlayers > 0.7;
                const isLocked = server.hasPassword;
                const lockIcon = isLocked ? '🔒 ' : '';
                const serverName = `${lockIcon} ${server.name}`;
                
                const serverDiv = document.createElement('div');
                
                let serverClass = 'server';
                let statusBadge = '';
                
                if (isLocked) {
                  serverClass += ' locked';
                } else if (isFull) {
                  serverClass += ' full';
                } else if (isActive) {
                  serverClass += ' active';
                } else {
                    serverClass += ' lowpop';
                }

                serverDiv.className = serverClass;

                serverDiv.innerHTML = `
                    <div class="server-header">
                        <h3><strong>${serverName}</strong></h3>
                        ${statusBadge}
                    </div>
                    <div class="server-meta">
                        <div><strong>Players:</strong> ${server.players}/${server.maxPlayers} (${server.queuePlayers} in queue)</div>
                        <div><strong>Game Mode:</strong> ${server.gamemode}</div>
                        <div><strong>Map:</strong> ${server.map}</div>
                        <div><strong>Map Size:</strong> ${server.mapSize}</div>
                        <div><strong>Password:</strong> ${server.hasPassword ? 'Yes' : 'No'}</div>
                        <div><strong>Official:</strong> ${server.isOfficial ? 'Yes' : 'No'}</div>
                        <div><strong>Tickrate:</strong> ${server.tickrate}</div>
                    </div>
                    `;
                serversContainer.appendChild(serverDiv);
            });
            serverListDiv.appendChild(regionDiv);
        });

    } catch (error) {
        console.error(error.message);
        statusDiv.textContent = "Error loading servers. Try again later.";
        statusDiv.style.color = "red";
    }
}